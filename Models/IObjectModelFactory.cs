using System.Threading.Tasks;

namespace appPDU.Models
{
    public interface IObjectModelFactory
    {
        Task<IObjectModel> GetObjectModel(IObjectModel model);
    }
}