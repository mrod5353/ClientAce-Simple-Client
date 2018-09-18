using System;
using Kepware.ClientAce.OpcDaClient;

namespace ClientAce_Simple_Client
{
    class Program
    {
        DaServerMgt daServerMgt = new DaServerMgt();
        ConnectInfo connectInfo = new ConnectInfo();

        static void Main(string[] args)
        {
            try
            {
                Program program = new Program();
                program.daServerMgt.DataChanged += program.DaServerMgt_DataChanged;
                program.Initialize();
            }
            finally
            {
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
        }

        private void Initialize()
        {
            connectInfo.LocalId = "en";
            connectInfo.KeepAliveTime = 5000;
            connectInfo.RetryAfterConnectionError = true;
            connectInfo.RetryInitialConnection = true;
            connectInfo.ClientName = "ClientAce Client";

            bool connectFailed;
            string url = "opcda://<computer name>/Kepware.KEPServerEX.V6";
            int clientHandle = 1;

            // Connect to server
            try
            {
                daServerMgt.Connect(url, clientHandle, ref connectInfo, out connectFailed);
                if (connectFailed)
                {
                    Console.WriteLine("Failed to connect to server. @ " + DateTime.Now);
                }
                else
                {
                    Console.WriteLine("Connected to Server " + url + " Succeeded." + DateTime.Now);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to server." + e + " @ " + DateTime.Now);
            }


            // Tags
            ItemIdentifier[] itemIdentifiers = new ItemIdentifier[2];
            itemIdentifiers[0] = new ItemIdentifier();
            itemIdentifiers[0].ItemName = "Channel1.Device1.Tag1";
            itemIdentifiers[0].ClientHandle = 1; // Assign unique handle

            itemIdentifiers[1] = new ItemIdentifier();
            itemIdentifiers[1].ItemName = "Simulation Examples.Functions.Ramp1";
            itemIdentifiers[1].ClientHandle = 2; // Assign unique handle

            int serverSubscription;
            ReturnCode returnCode;

            // Event subscription parameters
            int clientSubscription = 1;
            bool active = true;
            int updateRate = 1000;
            int revisedUpdateRate;
            float deadband = 0;

            try
            {
                //Subscribe to tags
                returnCode = daServerMgt.Subscribe(clientSubscription,
                                                    active,
                                                    updateRate,
                                                    out revisedUpdateRate,
                                                    deadband,
                                                    ref itemIdentifiers,
                                                    out serverSubscription);

                if (returnCode == ReturnCode.SUCCEEDED)
                {
                    Console.WriteLine("Successfully subscribed. @ " + DateTime.Now);
                }
                else
                {
                    Console.WriteLine("Subscritpion failed. @ " + DateTime.Now);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Subscritpion failed." + e + " @ " + DateTime.Now);
            }
        }

        private void DaServerMgt_DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
        {
            try
            {
                foreach (ItemValueCallback itemValue in itemValues)
                {
                    if (itemValue.ResultID.Succeeded)
                    {
                        Console.WriteLine(itemValue.ClientHandle + " : " + itemValue.Value);
                    }
                    else
                    {
                        Console.WriteLine("Subscription Callback failed @ " + DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataChanged exception. Reason: {0}", ex);
            }
        }
    }
}
