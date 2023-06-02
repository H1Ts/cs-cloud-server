using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        const int PORT = 8080;
        const int BUFFER_SIZE = 1024;

        TcpListener server = null;
        try
        {
            // Set the IP address and port on which the server will listen
            IPAddress ipAddress = IPAddress.Any;135.148.233.37
            server = new TcpListener(ipAddress, PORT);

            // Start listening for client connections
            server.Start();
            Console.WriteLine("Server listening on port " + PORT);

            // Accept client connections and handle requests
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");

                // Read client request
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
                string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received request: " + request);

                // Send response to client
                string response = "Hello from the server!";
                byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
                client.GetStream().Write(responseBuffer, 0, responseBuffer.Length);
                Console.WriteLine("Response sent");

                // Close the client connection
                client.Close();
                Console.WriteLine("Client disconnected");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            // Stop listening and clean up resources
            server?.Stop();
        }
    }
}
