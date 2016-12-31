using System;

namespace DiscordBot
{
    class Builder
    {
        private String name;
        private Build build;

        public Builder(String n)
        {
            name = n;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String n)
        {
            name = n;
        }

        public Build getBuild()
        {
            return build;
        }

        public void setBuild(Build b)
        {
            build = b;
        }
    }
}
