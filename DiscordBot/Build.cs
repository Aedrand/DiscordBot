using System;

namespace DiscordBot
{
    class Build
    {
        private String buildName;
        private String description;
        private String centPU;
        private String moBoard;
        private String graphPU;
        private String powSup;

        public Build(String n)
        {
            buildName = n;
        }

        public String getBuildName()
        {
            return buildName;
        }

        public void setBuildName(String bn)
        {
            buildName = bn;
        }

        public String getDescription()
        {
            return description;
        }

        public void setDescription(String d)
        {
            description = d;
        }

        public String getCentralPU()
        {
            return centPU;
        }

        public void setCentralPU(String cp)
        {
            centPU = cp;
        }

        public String getMoBoard()
        {
            return moBoard;
        }

        public void setMoBoard(String mb)
        {
            moBoard = mb;
        }

        public String getGraphicPU()
        {
            return graphPU;
        }

        public void setGraphicPU(String gp)
        {
            graphPU = gp;
        }

        public String getPowerSupp()
        {
            return powSup;
        }

        public void setPowerSupp(String ps)
        {
            powSup = ps;
        }
    }
}
