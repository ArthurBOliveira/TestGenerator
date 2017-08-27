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

            strings = text.Split(new string[] { "ResponseType(typeof(" }, StringSplitOptions.None);

            for (int i = 0; i < strings.Length - 1; i++)
            {
                string response = strings[i].Split(new string[] { "public" }, StringSplitOptions.None)[1].Split(new string[] { " " }, StringSplitOptions.None)[1].Trim();
                string name = strings[i].Split(new string[] { "public" }, StringSplitOptions.None)[1].Split(new string[] { " " }, StringSplitOptions.None)[2];

                method.Type = response;
                method.Name = name;

                if (method.Type != "void")
                {
                    if (i == 0 && (method.Type == "Guid" || method.Type == "int"))
                        method.IsKey = true;
                    Methods.Add(method);
                }

                method = new Property();
            }
        }

        private MethodType checkType()
        {

        }
    }
}
