# ClientAce-Simple-Client
This is a console application for a simple console application that uses ClientAce to connect to a Kepware server and subscribes to a tag and displays the data in real time.

Download and Installing KepServerEX -
<a href="https://www.kepware.com/en-us/content-gates/ex-demo-download-content-gate/?product=d2239b8c-36f2-4d07-8fbd-e223d0e26bbf&gate=d2a36dae-c6b1-453a-9093-ea51893ea76f">Download</a>

Make sure to select "Examples and Documentation" feature when installing the KepServerEX. This will download the tags that are used for the subscriptions in this example.

Check to see if KepServerEX is running locally in the taskbar.

Open the project in Visual Studio and replace the url variable in Program.cs with the correct connection url. 
opcda://<computer name>/Kepware.KEPServerEX.V6

The connection user needs to use the base opcda:// then the first part is the computers name followed by the servers name by default it will be KepwareKEPServer.V6 if you are running the latest KepServerEX

Hit F5 and run the application in Visual Studio.

Note: You may have to remove and add the Kepware.ClientAce.OpcClient.dll to the project
