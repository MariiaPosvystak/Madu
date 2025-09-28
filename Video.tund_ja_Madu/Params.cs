using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class Params
    {
        private string resourcesFoler;

        public Params()
        {
            var ind = Directory.GetCurrentDirectory().ToString()
                .IndexOf("bin", StringComparison.Ordinal); // Получить индекс папки bin

            string binFolder =
                Directory.GetCurrentDirectory().ToString().Substring(0, ind)
                    .ToString(); // путь до указанной в инкесе папки

            resourcesFoler = binFolder + "resources\\";
        }

        public string GetResourceFolder()
        {
            return resourcesFoler;
        }
    }
}
