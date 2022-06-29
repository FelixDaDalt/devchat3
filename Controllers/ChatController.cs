using devchat3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace devchat3.Controllers
{
    public class ChatController : Controller
    {

        public List<RoomResponse> GetRoomsAsync()
        {
            List<RoomResponse> roomChatList = null;
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string url = "https://localhost:7211/api/RoomChats/" + claim;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                roomChatList = JsonSerializer.Deserialize<List<RoomResponse>>(content,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );
            }
            return roomChatList;
        }

        [Authorize]
        public ActionResult<List<RoomResponse>> Index()
        {
            //Traer la lista de chats grupales
            List<RoomResponse> roomChatList = null;
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string url = "https://localhost:7211/api/RoomChats/" + claim;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                roomChatList = JsonSerializer.Deserialize<List<RoomResponse>>(content,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );
            }
            return View(roomChatList);
        }

        public IActionResult Room(int room, int category, string senderId, string receiverName)
        {
            ViewData["ChatList"] = GetRoomsAsync();
            //Aca le pido a la api el historial de mensajes de la sala
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            RoomResponse roomChat = new RoomResponse();
            //if (category == 1)
            //{
            string url = "https://localhost:7211/api/RoomChats/group/" + room;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                roomChat = JsonSerializer.Deserialize<RoomResponse>(content,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );
            }
            //}
            //if(category == 2) {
            //    //buscar el id por el nombre receiverName
            //    string url = "https://localhost:7211/api/RoomChats/priv?idSender=" + senderId
            //        + "&idReceiver=" + receiverName;
            //    HttpClient client = new HttpClient();
            //    var response = client.GetAsync(url).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = response.Content.ReadAsStringAsync().Result;
            //        roomChat = JsonSerializer.Deserialize<RoomResponse>(content,
            //            new JsonSerializerOptions()
            //            {
            //                PropertyNameCaseInsensitive = true
            //            }
            //            );
            //    }
            //}
            
            ViewData["MessageList"] = roomChat.Messages;
            // ??ViewData["UserList"] = 
            return View("Room", roomChat);
        }
    }
}
