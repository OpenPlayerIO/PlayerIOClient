using System.Linq;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;
using PlayerIOClient.Messages;
using PlayerIOClient.Messages.BigDB;

namespace PlayerIOClient
{
    public class BigDB
    {
        private const string PlayerObjectsTableName = "PlayerObjects";

        private readonly HttpChannel _channel;

        internal BigDB(HttpChannel channel)
        {
            _channel = channel;
        }

        public DatabaseObject LoadMyPlayerObject()
        {
            var loadMyPlayerObjectOutput = _channel.Request<NoArgsOrOutput, LoadMyPlayerObjectOutput, PlayerIOError>(103, new NoArgsOrOutput());
            loadMyPlayerObjectOutput.PlayerObject.Table = PlayerObjectsTableName;

            return loadMyPlayerObjectOutput.PlayerObject;
        }

        public DatabaseObject Load(string table, string key) => Load(table, new[] { key })[0];

        public DatabaseObject[] Load(string table, string[] keys)
        {
            var loadObjectsOutput = _channel.Request<LoadObjectsArgs, LoadObjectsOutput, PlayerIOError>(85, new LoadObjectsArgs {
                ObjectIds = new BigDBObjectId { Table = table, Keys = keys }
            });
            
            foreach (var obj in loadObjectsOutput.Objects)
                obj.Table = table;
            
            return loadObjectsOutput.Objects;
        }

        public DatabaseObject LoadOrCreate(string table, string[] keys) => CreateObjects((from key in keys select new DatabaseObject() { Key = key, Table = table }).ToArray(), true).FirstOrDefault();

        public DatabaseObject CreateObject(string table, string key, DatabaseObject obj) => CreateObjects(new[] { new DatabaseObject() { Table = table, Key = key, Properties = obj.Properties } }, false).FirstOrDefault();

        internal DatabaseObject[] CreateObjects(DatabaseObject[] objects, bool loadExisting)
        {
            var output = _channel.Request<CreateObjectsArgs, CreateObjectsOutput, PlayerIOError>(82, new CreateObjectsArgs {
                LoadExisting = loadExisting,
                Objects = objects
            });

            return output.Objects;
        }
    }
}