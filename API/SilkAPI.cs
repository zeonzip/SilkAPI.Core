using System;
using System.Collections.Generic;
using System.Text;

namespace NSilkAPI.API
{
    public class SilkApi
    {
        private SilkApi()
        {
        }
        public static SilkApi instance = new SilkApi();

        public Registries.Registries Registries = new Registries.Registries();

    }
}
