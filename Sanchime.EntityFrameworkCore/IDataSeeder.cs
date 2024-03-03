namespace Sanchime.EntityFrameworkCore;

public interface IDataSeeder
{
    ValueTask Initialize();
}
