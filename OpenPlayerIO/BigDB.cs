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

        /// <summary> Load a range of database objects from a table using the specified index. </summary>
        /// <param name="table"> The table to load the database object from. </param>
        /// <param name="index"> The name of the index to query for the database object. </param>
        /// <param name="indexPath"> Where in the index to start the range search: An array of objects of the same types as the index properties, specifying where in the index to start loading database objects from. IndexPath can be set to null if there is only one property in the index.</param>
        /// <param name="start"> Where to start the range search. For instance, if the index is [Mode,Map,Score] and indexPath is ["expert","skyland"], then start defines the minimum score to include in the results</param>
        /// <param name="stop"> Where to stop the range search. </param>
        /// <param name="limit"> The max amount of objects to return. </param>
        /// <param name="successCallback"> A callback containing the database objects found. </param>
        public DatabaseObject[] LoadRange(string table, string index, object[] indexPath, object start, object stop, int limit)
        {
            var startIndex = indexPath.Select(BigDBObjectValue.Create).ToList();
                if (start != null) startIndex.Add(BigDBObjectValue.Create(start));

            var stopIndex = indexPath.Select(BigDBObjectValue.Create).ToList();
                if (stop != null) stopIndex.Add(BigDBObjectValue.Create(stop));

            var loadRangeOutput = _channel.Request<LoadIndexRangeArgs, LoadObjectsOutput, PlayerIOError>(97, new LoadIndexRangeArgs {
                Table = table,
                Index = index,
                StartIndexValue = startIndex.ToArray(),
                StopIndexValue = stopIndex.ToArray(),
                Limit = limit,
            });

            foreach (var obj in loadRangeOutput.Objects)
                obj.Table = table;

            return loadRangeOutput.Objects;
        }

        public DatabaseObject LoadOrCreate(string table, string key) => CreateObjects(new[] { new SentDatabaseObject() { Key = key, Table = table } }, true).FirstOrDefault();

        public DatabaseObject[] LoadKeysOrCreate(string table, string[] keys) => CreateObjects((from key in keys select new SentDatabaseObject() { Key = key, Table = table }).ToArray(), true);

        public DatabaseObject CreateObject(string table, string key, DatabaseObject obj)
        {
            var createObjectsOutput = CreateObjects(new[] { new SentDatabaseObject() { Table = table, Key = key, Properties = obj.Properties } }, false);

            return createObjectsOutput.FirstOrDefault();
        }

        internal DatabaseObject[] CreateObjects(SentDatabaseObject[] objects, bool loadExisting)
        {
            var output = _channel.Request<CreateObjectsArgs, CreateObjectsOutput, PlayerIOError>(82, new CreateObjectsArgs {
                LoadExisting = loadExisting,
                Objects = objects
            });

            return output.Objects;
        }
    }
}