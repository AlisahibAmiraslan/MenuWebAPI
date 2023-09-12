using MenuWebAPI.Models;

namespace MenuWebAPI.Interfaces
{
    public interface IHeaderMenu
    {

        ICollection<HeaderMenu> GetHeaderMenu();
        HeaderMenu GetHeaderMenu(int id);
        bool CreateHeaderMenu(HeaderMenu menu);
        bool UpdateHeaderMenu(HeaderMenu menu);
        bool DeleteHeaderMenu(HeaderMenu menu);
        
    }
}
