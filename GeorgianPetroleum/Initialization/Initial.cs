using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgianPetroleum.Initialization
{
    class Initial : IRunable
    {
        public void Run(DiManager diManager)
        {
            IEnumerable<IRunable> objects = new List<IRunable>()
            {
                new CreateTables(),
                new CreateFields(),
                new AddKeys()
            };
            foreach (IRunable item in objects)
            {
                item.Run(diManager);
            }
        }
    }
}
