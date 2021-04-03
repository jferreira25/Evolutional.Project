using System.Collections.Generic;

namespace Evolutional.Project.Domain.Services.Xlsx
{
    public interface ISheetsService
    {
        byte[] Generate(string sheetName, IEnumerable<object> objects);
    }
}
