using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public class Method
    {
        public string Name { get; set; }
        public MethodType Type { get; set; }
        public List<Params> Params { get; set; }
    }

    public enum MethodType
    {
        Get,
        List,
        Post,
        Put,
        Delete
    }

    public class Params
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsObj { get; set; }
        public Model Model { get; set; }
    }
}