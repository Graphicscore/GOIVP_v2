using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class OIVField : System.Attribute
    {
        private Boolean required;
        private String displayText;

        public OIVField(Boolean required, String displayText = null)
        {
            this.required = required;
            this.displayText = displayText;
        }

        public String getDisplayText()
        {
            return this.displayText;
        }

        public class Container
        {
            public List<GOIVPropertyContainer> validate()
            {
                List<GOIVPropertyContainer> invalidList = new List<GOIVPropertyContainer>();

                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.IsDefined(typeof(OIVField), true))
                    {
                        Object[] attributes = property.GetCustomAttributes(typeof(OIVField), true);
                        if (attributes.Length > 0 && attributes[0] != null)
                        {
                            OIVField attribute = attributes[0] as OIVField;
                            if (!attribute.required)
                            {
                                continue;
                            }
                            else
                            {
                                Object value = property.GetValue(this, null);
                                if (value != null && value.GetType() == typeof(String))
                                {
                                    if (string.IsNullOrEmpty(value as String) || string.IsNullOrWhiteSpace(value as String))
                                    {
                                        //throw new InvalidOIVStringException(this, property);
                                        invalidList.Add(new GOIVPropertyContainer(property, attribute));
                                    }
                                }
                                else
                                {
                                    if (value == null)
                                    {
                                        //throw new InvalidOIVException(this, property);
                                        invalidList.Add(new GOIVPropertyContainer(property, attribute));
                                    }
                                    else
                                    {
                                        if (value.GetType().IsSubclassOf(typeof(Container)))
                                        {
                                            invalidList.AddRange((value as Container).validate());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return invalidList;
            }
        }
    }

    public class GOIVPropertyContainer
    {
        public PropertyInfo info;
        public OIVField attribute;

        public GOIVPropertyContainer(PropertyInfo info, OIVField attribute)
        {
            this.info = info;
            this.attribute = attribute;
        }
    }

    public class InvalidOIVStringException : InvalidOIVException
    {
        public InvalidOIVStringException(Object obj, PropertyInfo propety) : base(obj, propety)
        {

        }
    }

    public class InvalidOIVException : Exception
    {
        public Object obj;
        public PropertyInfo info;


        public InvalidOIVException(Object obj, PropertyInfo propety)
        {
            this.obj = obj;
            this.info = propety;
        }
    }
}
