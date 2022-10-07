using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaBigFeet
{
    class ClsProducto
    {
        public int id { get; set; }
        public string marca { get; set; }
        public string descripcion { get; set; }
        public string categoria { get; set; }
        public double talla { get; set; }
        public int cantidad { get; set; }
        public double pCompra { get; set; }
        public double pVenta { get; set; }
    }
}
