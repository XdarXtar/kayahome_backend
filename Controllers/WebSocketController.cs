using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace kayahome_backend.Controllers
{
    [Route("ws")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        [HttpGet("connect")]
        public async Task ConnectWebSocket()
        {
            var serverUri = new Uri("wss://echo.websocket.org");
            using var clientWebSocket = new ClientWebSocket();

            try
            {
                await clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("WebSocket connected!");

                // Send a message
                var messageToSend = "Hello, WebSocket!";
                var sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageToSend));
                await clientWebSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

                // Receive the echoed message
                var receiveBuffer = new byte[1024];
                var receiveResult = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
                Console.WriteLine($"Received: {receivedMessage}");

                // Send a message
                messageToSend = "Hello, WebSocket!";
                sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageToSend));
                await clientWebSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

                // Receive the echoed message
                receiveBuffer = new byte[1024];
                receiveResult = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
                Console.WriteLine($"Received: {receivedMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                Console.WriteLine("WebSocket closed!");
            }
        }
    }
}
