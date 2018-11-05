using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public interface IDataSetRepository
    {
        Task<DataSet> HashExists(string hash);

        Task<int> InsertDataSet(DataSet dataSet);

        Task<IEnumerable<UserFriendship>> GetUserFriendship(int dataSetId);
    }
}
