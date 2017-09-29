using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace License3DotNet.utils
{
    class CommandLineProcessor
    {
        private Dictionary<String, String> options = new Dictionary<String, String>();
        private List<String> files = new List<String>();

        /**
         * Get the options that were gathered by the method {@code process()}.
         * 
         * @return the map containing the command line argument key value pairs
         */
        public Dictionary<String, String> getOptions()
        {
            return options;
        }

        /**
         * Decides if a command line option was specified on the command line or
         * not.
         * <p>
         * Should only be called after the method {@code process()} was invoked.
         * 
         * @param name
         *            the name of the option we are looking for
         * @return {@code true} if the option was specified on the command line, and
         *         {@code false} if the option was not specified
         */
        public Boolean optionExists(String name)
        {
            return getOptions().ContainsKey(name);
        }

        /**
         * Get a command line option value.
         * <p>
         * Should only be called after the method {@code process()} was invoked.
         * 
         * @param name
         *            the name of the option we are looking for
         * 
         * @return the value of the option
         */
        public String option(String name)
        {
            return getOptions()[name];
        }

        /**
         * Get the non option (file) arguments that were gathered by the method
         * {@code process()}.
         * 
         * @return the list of the arguments that do not have name in the order as
         *         they appeared on the command line
         */
        public List<String> getFiles()
        {
            return files;
        }

        /**
         * Parses the command line to discover all the 'option' arguments and all
         * the 'file' arguments.
         * 
         * When an argument starts with double dash {@code --} then this is a
         * 
         * <pre>
         * --arg=value
         * </pre>
         * 
         * formatted argument. The value and the name of the option are separated
         * using an {@code =} equal sign and thus present in the same command line
         * argument.
         * <P>
         * When the argument starts with single dash {@code -} (and not double dash
         * {@code --}) then it is a
         * 
         * <pre>
         * -arg value
         * </pre>
         * 
         * type argument. The difference is that in this case the argument value is
         * the next command line argument separated by space on the command line
         * from the name of the options.
         * <p>
         * When an argument does not start with dash then this is a 'file' parameter
         * and it gets into the {@code files} collection.
         * <p>
         * When a 'file' argument starts with a dash '-' then it has to be preceded
         * with two dashes. For example {@code -specialFile} has to be written as
         * {@code ---specialFile}.
         * <p>
         * The order of the parameters and the way they are specified ( -- or - ) is
         * not preserved. The order of 'file' arguments is reserved in the array.
         * 
         * @param args
         *            the arguments passed to the main function
         */
        public void process(String[] args) {
            for (int i = 0; args != null && i < args.Length; i++)
            {
                String arg = args[i];

                if (arg.StartsWith("---"))
                {
                    files.Add(arg.Substring(2));
                }
                else if (arg.StartsWith("--"))
                {
                    arg = arg.Substring(2);
                    int indexOfEqualSign = arg.IndexOf("=");
                    if (indexOfEqualSign == -1)
                    {
                        options.Add(arg, null);
                    }
                    else
                    {
                        String val = arg.Substring(indexOfEqualSign + 1);
                        arg = arg.Substring(0, indexOfEqualSign);
                        options.Add(arg, val);
                    }
                }
                else if (arg.StartsWith("-"))
                {
                    arg = arg.Substring(1);
                    if (i + 1 < args.Length)
                    {
                        options.Add(arg, args[i + 1]);
                    }
                    else
                    {
                        options.Add(arg, null);
                    }
                }
                else
                {
                    files.Add(arg);
                }
            }
        }
    }
}
