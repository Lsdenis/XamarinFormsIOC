using System.Threading.Tasks;

namespace XamarinFormsIOC.Interfaces
{
    public interface IInitViewModel<in T> where T : class 
    {
        Task InitModel(T data);
    }
}