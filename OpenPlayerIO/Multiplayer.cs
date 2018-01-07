using System;
using System.Collections.Generic;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;
using PlayerIOClient.Messages.Client;

namespace PlayerIOClient
{
    public class Multiplayer
    {
        /// <summary>
        /// If not null, rooms will be created on the development server at the address defined by
        /// the server endpoint, instead of using the live Player.IO serveer.
        /// </summary>
        public ServerEndpoint DevelopmentServer { get; set; }

        [Obsolete("Please use DevelopmentServer property instead")]
        public ServerEndpoint DevServer
        {
            get { return this.DevelopmentServer; }
            set { this.DevelopmentServer = value; }
        }

        public MultiplayerProxy Proxy { get; set; }

        private readonly HttpChannel _channel;
        internal Multiplayer(HttpChannel channel)
        {
            _channel = channel;
        }

        /// <summary> Creates a multiplayer room (if it doesn't exists already), and joins it. </summary>
        /// <param name="roomId"> The ID of the room you wish to (create and then) join. </param>
        /// <param name="serverType">
        /// If the room doesn't exists: The name of the room type you wish to run the room as. This
        /// value should match one of the 'RoomType(...)' attributes of your uploaded code. A room
        /// type of 'bounce' is always available.
        /// </param>
        /// <param name="visible">
        /// If the room doesn't exists: Determines (upon creation) if the room should be visible when
        /// listing rooms with GetRooms.
        /// </param>
        /// <param name="roomData">
        /// If the room doesn't exists: The data to initialize the room with (upon creation).
        /// </param>
        /// <param name="joinData">
        /// Data to send to the room with additional information about the join.
        /// </param>
        public Connection CreateJoinRoom(string roomId, string serverType, bool visible = true, Dictionary<string, string> roomData = null, Dictionary<string, string> joinData = null)
        {
            var createJoinRoomArg = new CreateJoinRoomArgs {
                RoomId = roomId,
                ServerType = serverType,
                Visible = visible,
                RoomData = Converter.Convert(roomData),
                JoinData = Converter.Convert(joinData),
                IsDevRoom = this.DevelopmentServer != null
            };

            var createJoinRoomOutput = _channel.Request<CreateJoinRoomArgs, CreateJoinRoomOutput, PlayerIOError>(27, createJoinRoomArg);
            var serverEndpoint = this.DevelopmentServer ?? Converter.Convert(createJoinRoomOutput.Endpoints[0]);

            return new Connection(serverEndpoint, createJoinRoomOutput.JoinKey, this.Proxy);
        }

        /// <summary> Creates a multiplayer room (if it doesn't exists already), and joins it. </summary>
        /// <param name="roomId"> The ID of the room you wish to (create and then) join. </param>
        /// <param name="serverType">
        /// If the room doesn't exists: The name of the room type you wish to run the room as. This
        /// value should match one of the 'RoomType(...)' attributes of your uploaded code. A room
        /// type of 'bounce' is always available.
        /// </param>
        /// <param name="visible">
        /// If the room doesn't exists: Determines (upon creation) if the room should be visible when
        /// listing rooms with GetRooms.
        /// </param>
        /// <param name="roomData">
        /// If the room doesn't exists: The data to initialize the room with (upon creation).
        /// </param>
        /// <param name="joinData">
        /// Data to send to the room with additional information about the join.
        /// </param>
        /// <param name="successCallback"> A callback called when you successfully created and joined the room. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> if an error occurs when creating and joining the room. </param>
        public void CreateJoinRoom(string roomId, string serverType, bool visible = true, Dictionary<string, string> roomData = null, Dictionary<string, string> joinData = null, Callback<Connection> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var createJoinRoomArg = new CreateJoinRoomArgs {
                RoomId = roomId,
                ServerType = serverType,
                Visible = visible,
                RoomData = Converter.Convert(roomData),
                JoinData = Converter.Convert(joinData),
                IsDevRoom = this.DevelopmentServer != null
            };

            var createJoinRoomOutput = _channel.Request<CreateJoinRoomArgs, CreateJoinRoomOutput, PlayerIOError>(27, createJoinRoomArg, errorCallback: errorCallback);
            
            if (createJoinRoomOutput != null) {
                var serverEndpoint = this.DevelopmentServer ?? Converter.Convert(createJoinRoomOutput.Endpoints[0]);

                successCallback(new Connection(serverEndpoint, createJoinRoomOutput.JoinKey, this.Proxy));
            }
        }

        /// <summary> Joins a running multiplayer room. </summary>
        /// <param name="roomId"> The ID of the room you wish to join. </param>
        /// <param name="joinData">
        /// Data to send to the room with additional information about the join.
        /// </param>
        public Connection JoinRoom(string roomId, Dictionary<string, string> joinData = null)
        {
            var joinRoomArg = new JoinRoomArgs {
                RoomId = roomId,
                JoinData = Converter.Convert(joinData),
                IsDevRoom = this.DevelopmentServer != null
            };

            var joinRoomOutput = _channel.Request<JoinRoomArgs, JoinRoomOutput, PlayerIOError>(24, joinRoomArg);
            var serverEndpoint = this.DevelopmentServer ?? Converter.Convert(joinRoomOutput.Endpoints[0]);

            return new Connection(serverEndpoint, joinRoomOutput.JoinKey, this.Proxy);
        }

        /// <summary> Joins a running multiplayer room. </summary>
        /// <param name="roomId"> The ID of the room you wish to join. </param>
        /// <param name="joinData">
        /// Data to send to the room with additional information about the join.
        /// </param>
        /// <param name="successCallback"> A callback called when you successfully created and joined the room. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> if an error occurs when joining the room. </param>
        public void JoinRoom(string roomId, Dictionary<string, string> joinData = null, Callback<Connection> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var joinRoomArg = new JoinRoomArgs {
                RoomId = roomId,
                JoinData = Converter.Convert(joinData),
                IsDevRoom = this.DevelopmentServer != null
            };

            var joinRoomOutput = _channel.Request<JoinRoomArgs, JoinRoomOutput, PlayerIOError>(24, joinRoomArg);

            if (joinRoomOutput != null) {
                var serverEndpoint = this.DevelopmentServer ?? Converter.Convert(joinRoomOutput.Endpoints[0]);

                successCallback(new Connection(serverEndpoint, joinRoomOutput.JoinKey, this.Proxy));
            }
        }

        /// <summary> Lists the currently running multiplayer rooms. </summary>
        /// <param name="roomType"> The type of the rooms you wish to list. </param>
        /// <param name="searchCriteria">
        /// Only rooms with the same values in their roomData will be returned.
        /// </param>
        /// <param name="resultLimit">
        /// The maximum amount of rooms you want to receive. Use 0 for 'as many as possible'.
        /// </param>
        /// <param name="resultOffset"> Defines the index to show results from. </param>
        /// <param name="onlyDevRooms">
        /// Set to 'true' to list rooms from the development room list, rather than from the game's
        /// global room list.
        /// </param>
        public RoomInfo[] ListRooms(string roomType, Dictionary<string, string> searchCriteria = null, int resultLimit = 0, int resultOffset = 0, bool onlyDevRooms = false)
        {
            var listRoomsArg = new ListRoomsArgs {
                RoomType = roomType,
                SearchCriteria = Converter.Convert(searchCriteria),
                ResultLimit = resultLimit,
                ResultOffset = resultOffset,
                OnlyDevRooms = onlyDevRooms
            };
            var listRoomsOutput = _channel.Request<ListRoomsArgs, ListRoomsOutput, PlayerIOError>(30, listRoomsArg);

            return listRoomsOutput.RoomInfo ?? new RoomInfo[0];
        }

        /// <summary> Lists the currently running multiplayer rooms. </summary>
        /// <param name="roomType"> The type of the rooms you wish to list. </param>
        /// <param name="searchCriteria">
        /// Only rooms with the same values in their roomData will be returned.
        /// </param>
        /// <param name="resultLimit">
        /// The maximum amount of rooms you want to receive. Use 0 for 'as many as possible'.
        /// </param>
        /// <param name="resultOffset"> Defines the index to show results from. </param>
        /// <param name="onlyDevRooms">
        /// Set to 'true' to list rooms from the development room list, rather than from the game's
        /// global room list.
        /// </param>
        /// <param name="successCallback"> A callback called when the search was successful. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during the search. </param>
        public void ListRooms(string roomType, Dictionary<string, string> searchCriteria = null, int resultLimit = 0, int resultOffset = 0, bool onlyDevRooms = false, Callback<RoomInfo[]> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var listRoomsArg = new ListRoomsArgs {
                RoomType = roomType,
                SearchCriteria = Converter.Convert(searchCriteria),
                ResultLimit = resultLimit,
                ResultOffset = resultOffset,
                OnlyDevRooms = onlyDevRooms
            };
            var listRoomsOutput = _channel.Request<ListRoomsArgs, ListRoomsOutput, PlayerIOError>(30, listRoomsArg);

            if (listRoomsOutput != null)
                successCallback(listRoomsOutput.RoomInfo ?? new RoomInfo[0]);
        }
    }
}