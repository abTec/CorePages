using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Newtonsoft.Json;
using RandomNameGenerator;

namespace Services
{
    public class DataSetService : IDataSetService
    {
        private readonly IDataSetRepository dataSetRepository;

        public DataSetService(IDataSetRepository dataSetRepository)
        {
            this.dataSetRepository = dataSetRepository;
        }


        public async Task<DataSet> CreateDataSetAsync(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var hash = streamReader.BaseStream.ComputeHash();


                var set = await dataSetRepository.HashExists(hash);
                if (set != null)
                {
                    throw new Exception($"Given File already imported on {set.InseretedDateUtc} with id {set.ID}.");
                }

                var dataSet = new DataSet {Hash = hash, Users = new List<User>()};

                streamReader.DiscardBufferedData();
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                while (streamReader.Peek() >= 0)
                {
                    var line = await streamReader.ReadLineAsync();
                    var ids = line.Split(' ');
                    if (ids.Length != 2)
                    {
                        throw new Exception("Wrong File Format!");
                    }

                    int.TryParse(ids[0], out var userId);
                    var user = dataSet.Users.FirstOrDefault(us => us.ID == userId) ?? CreateUser(userId);

                    int.TryParse(ids[1], out var friendId);
                    var friend = dataSet.Users.FirstOrDefault(us => us.ID == friendId) ?? CreateUser(friendId);

                    if (dataSet.Users.All(u => u.ID != user.ID))
                        dataSet.Users.Add(user);
                    if (dataSet.Users.All(u => u.ID != friend.ID))
                        dataSet.Users.Add(friend);

                    if (user.Friends.All(f => f.FriendId != friend.ID))
                    {
                        user.Friends.Add(new UserFriendship
                        {
                            UserID = user.ID,
                            FriendId = friend.ID
                        });
                        user.NumberOfFriends++;
                    }

                    if (friend.Friends.All(f => f.FriendId != user.ID))
                    {
                        friend.Friends.Add(new UserFriendship
                        {
                            UserID = friend.ID,
                            FriendId = user.ID
                        });
                        friend.NumberOfFriends++;
                    }

                }

                dataSet.NumberOfUsers = dataSet.Users.Count;

                return dataSet;
            }
        }

        public async Task<string> DataSetToJson(DataSet dataSet)
        {
            var nodes = dataSet.Users.Select(u => new Node(u.Name, u.ID.ToString()));

            var edges = await dataSetRepository.GetUserFriendship(dataSet.ID);
            var edgesss = edges
                    .Select(edge => new Edge(edge.UserID.ToString(), edge.FriendId.ToString()));
            var graph = new Graph
            {
                Nodes = nodes.ToArray(),
                Edges = edgesss.ToList()
            };
            return JsonConvert.SerializeObject(graph, Formatting.Indented);
        }

        private User CreateUser(int userId)
            =>
                new User
                {
                    ID = userId,
                    Name = NameGenerator.Generate(userId % 2 == 0 ? Gender.Male : Gender.Female),
                    Friends = new List<UserFriendship>()
                };
    }

    public class Graph
    {
        public Graph()
        {
            Edges = new List<Edge>();
        }
        [JsonProperty("nodes")]
        public Node[] Nodes { get; set; }
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    public class Node
    {
        public Node(string label, string id)
        {
            this.Label = label;
            this.Id = id;
        }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("size")] public int Size { get; set; } = 6;
    }

    public class Edge
    {
        public Edge(string source, string target)
        {
            Id = $"{source}.{target}";
            Source = source;
            Target = target;
        }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
