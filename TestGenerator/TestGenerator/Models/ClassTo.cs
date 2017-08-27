using System;
using System.Collections.Generic;

namespace TestGenerator
{
    public class ClassTo
    {
        public string Name { get; set; }
        public List<Method> Methods { get; set; }
        public Model Model { get; set; }
        public string ProjectName { get; set; }

        public ClassTo(string text, string projectName)
        {
            string[] strings;
            Method method = new Method();

            ProjectName = projectName;

            Name = text.Split(new string[] { " " }, StringSplitOptions.None)[1].Split(new string[] { "\r" }, StringSplitOptions.None)[0];

            Methods = new List<Method>();

            strings = text.Split(new string[] { "<summary>" }, StringSplitOptions.None);

            for (int i = 1; i < strings.Length; i++)
            {
                MethodType type;

                string auxType = strings[i].Split(new string[] { "/// " }, StringSplitOptions.None)[1].Split(new string[] { " " }, StringSplitOptions.None)[0];

                if (Enum.TryParse(auxType, out type))
                {
                    method.Type = type;
                    method.Params = new List<Params>();

                    string name = strings[i].Split(new string[] { "public" }, StringSplitOptions.None)[1].Split(new string[] { " " }, StringSplitOptions.None)[2].Split('(')[0];
                    method.Name = name;

                    string[] _params = strings[i].Split(new string[] { "public" }, StringSplitOptions.None)[1].Split(')')[0].Split('(')[1].Split(new string[] { ", " }, StringSplitOptions.None);

                    if (_params[0] != "")
                    {
                        for (int j = 0; j < _params.Length; j++)
                        {
                            Params aux = new Params();

                            _params[j] = _params[j].Replace("[FromBody]", "");
                            _params[j] = _params[j].Replace("[FromODataUri]", "");
                            _params[j] = _params[j].Replace("[FromUri]", "");

                            aux.Name = _params[j].Split(new string[] { " " }, StringSplitOptions.None)[1];
                            aux.Type = _params[j].Split(new string[] { " " }, StringSplitOptions.None)[0];

                            aux.IsObj = CheckIfObj(aux.Type);

                            method.Params.Add(aux);
                            aux = new Params();
                        }
                    }

                    Methods.Add(method);

                    method = new Method();
                }
            }
        }

        private bool CheckIfObj(string type)
        {
            if (type.Contains("Guid"))
                return false;
            if (type.Contains("Int"))
                return false;
            if (type.Contains("Bool"))
                return false;
            if (type.Contains("Guid"))
                return false;
            if (type.Contains("bool"))
                return false;
            if (type.Contains("Boolean"))
                return false;
            if (type.Contains("boolean"))
                return false;
            if (type.Contains("DateTime"))
                return false;
            if (type.Contains("String"))
                return false;
            if (type.Contains("string"))
                return false;
            if (type.Contains("Float"))
                return false;
            if (type.Contains("float"))
                return false;
            if (type.Contains("Double"))
                return false;
            if (type.Contains("double"))
                return false;
            if (type.Contains("Decimal"))
                return false;
            if (type.Contains("decimal"))
                return false;

            return true;
        }
    }
}
