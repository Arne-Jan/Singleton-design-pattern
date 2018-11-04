using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    /*
    // Singleton
    public class PlayerStatsSingleton
    {
        private static PlayerStatsSingleton uniqueInstance;

        // Private constructor
        private PlayerStatsSingleton() { }

        public static PlayerStatsSingleton GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new PlayerStatsSingleton();
            }
            return uniqueInstance;
        }
    }

    // Probleem? Niet thread safe!
    public class PlayerStatsSingleton
    {
        private static PlayerStatsSingleton uniqueInstance;

        private PlayerStatsSingleton() { }

        public static PlayerStatsSingleton GetInstance()
        {
            // Toegang tot uniqueInstance door verschillende threads tegelijk is mogelijk
            if (uniqueInstance == null)
            {
                // Zo kan dus alsnog meer dan een keer een nieuwe PlayerStatsSingleton worden aangemaakt 
                uniqueInstance = new PlayerStatsSingleton();
            }
            return uniqueInstance;
        }
    }

    // Singleton: Thread safe
    public class PlayerStatsSingleton
    {
        private static PlayerStatsSingleton uniqueInstance;

        // Het "slot" om de toegang tot uniqueInstance mee te synchroniseren
        private static readonly object singletonLock = new object();

        private PlayerStatsSingleton() { }

        public static PlayerStatsSingleton GetInstance()
        {
            // Toegang tot uniqueInstance synchroniseren door deze "op slot" te zetten met singletonLock
            lock (singletonLock)
            {
                // uniqueInstance kan nu niet worden aangeroepen door meerdere threads tegelijk
                if (uniqueInstance == null)
                {
                    // Nu kan er dus echt maar één PlayerStatsSingleton aan worden gemaakt, zoals gewenst
                    uniqueInstance = new PlayerStatsSingleton();
                }
                return uniqueInstance;
            }
        }
    }

    // Probleem? Performance
    public class PlayerStatsSingleton
    {
        private static PlayerStatsSingleton uniqueInstance;

        private static readonly object singletonLock = new object();

        private PlayerStatsSingleton() { }

        public static PlayerStatsSingleton GetInstance()
        {
            // Lock is nodig elke keer als we GetInstance() aanroepen
            lock (singletonLock)
            {
                if (uniqueInstance == null)
                {
                    uniqueInstance = new PlayerStatsSingleton();
                }
                return uniqueInstance;
            }
        }
    }

    // Singleton: Double check locked
    public class PlayerStatsSingleton
    {
        private static PlayerStatsSingleton uniqueInstance;

        private static readonly object singletonLock = new object();

        private PlayerStatsSingleton() { }

        public static PlayerStatsSingleton GetInstance()
        {
            // Eerst checken of uniqueInstance al geïnstantieerd is
            if (uniqueInstance == null)
            {
                // Nee? Alleen dan hoeft er gesynchroniseerd te worden
                lock (singletonLock)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new PlayerStatsSingleton();
                    }
                }
            }
            // Als uniqueInstance al bestaat, kunnen we deze simpelweg teruggeven
            return uniqueInstance;
        }
    }
    */

    public class PlayerStatsSingleton
    {
        private static PlayerStatsSingleton _uniqueInstance;

        private static readonly object SingletonLock = new object();

        private PlayerStatsSingleton() { }

        public static PlayerStatsSingleton GetInstance()
        {
            if (_uniqueInstance != null) return _uniqueInstance;
            lock (SingletonLock)
            {
                if (_uniqueInstance == null)
                {
                    _uniqueInstance = new PlayerStatsSingleton();
                }
            }
            return _uniqueInstance;
        }

        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
    }
}
