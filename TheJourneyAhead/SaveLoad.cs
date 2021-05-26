using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using System.Xml.Serialization;

namespace TheJourneyAhead
{
    public class SaveLoad
    {
        IAsyncResult result;
        StorageDevice device;
        bool GameSaveLoadRequested;
        bool deleteRequested;
        static bool saved;
        static bool deleted;
        [Serializable]
        public struct SaveGameData
        {
            public string area;
            public string gender;
            public int hair;
            public int hairColor;
            public int skin;
            public int head;
            public int eyes;
            public int eyeColor;
            public int pPositionX;
            public int pPositionY;
            public int areaX;
            public int areaY;
            public string pName;
            public bool fromPortal;
            public int portalID;
            public List<MapContents> mapContents;
            
        }

        public bool Saved //Returns if item has been clicked
        {
            get { return saved; }
        }

        public bool Deleted //Returns if item has been clicked
        {
            get { return deleted; }
        }

        public void Save(string Area, string Gender, int Hair, int HairColor, int Skin, int Head, int Eyes, int EyesColor, int PPositionX, int PPositionY, int AreaX, int AreaY, string PName, bool FromPortal, int PortalID)
        {
            saved = false;
            GameSaveLoadRequested = true;
            result = StorageDevice.BeginShowSelector(
                            PlayerIndex.One, null, null);
            if ((GameSaveLoadRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    DoSaveGame(device, Area, Gender, Hair, HairColor, Skin, Head, Eyes, EyesColor, PPositionX, PPositionY, AreaX, AreaY, PName, FromPortal, PortalID);
                    
                }
            }
            
            
        }

        public void Load(string fileName, InputManager inputManager)
        {            
            GameSaveLoadRequested = true;
            result = StorageDevice.BeginShowSelector(
                            PlayerIndex.One, null, null);
            if ((GameSaveLoadRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    DoLoadGame(device, fileName);
                    Type newClass = Type.GetType("TheJourneyAhead." + "GameplayScreen");
                    ScreenManager.Instance.AddScreen((Screens)Activator.CreateInstance(newClass), inputManager);
                }
            }

        }

        public void Delete(string fileName)
        {
            deleted = false;
            deleteRequested = true;
            result = StorageDevice.BeginShowSelector(
                            PlayerIndex.One, null, null);
            if ((deleteRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    DoDelete(device, fileName);
                }
            }
        }

        private static void DoSaveGame(StorageDevice device, string Area, string Gender, int Hair, int HairColor, int Skin, int Head, int Eyes, int EyeColor, int PPositionX, int PPositionY, int AreaX, int AreaY, string PName, bool FromPortal, int PortalID)
        {
            SaveGameData data = new SaveGameData();
            data.area = Area;
            data.gender = Gender;
            data.hair = Hair;
            data.hairColor = HairColor;
            data.skin = Skin;
            data.head = Head;
            data.eyes = Eyes;
            data.eyeColor = EyeColor;
            data.pPositionX = PPositionX;
            data.pPositionY = PPositionY;
            data.areaX = AreaX;
            data.areaY = AreaY;
            data.pName = PName;
            data.fromPortal = FromPortal;
            data.portalID = PortalID;
            data.mapContents = LocationsManager.Instance.MapContents;
            
            // Open a storage container.
            IAsyncResult result =
             device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = PName + ".sav";
            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            serializer.Serialize(stream, data);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
            saved = true;
        }

        private static void DoLoadGame(StorageDevice device, string fileName)
        {
            FileManager fileManager = new FileManager();
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string Filename = fileName + ".sav";

            // Check to see whether the save exists.
            if (!container.FileExists(Filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }

            // Open the file.
            Stream stream = container.OpenFile(Filename, FileMode.Open);

            // Read the data from the file.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            SaveGameData data = (SaveGameData)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            PlayerDetails.Instance.Area = data.area;
            PlayerDetails.Instance.Gender = data.gender;
            PlayerDetails.Instance.Hair = data.hair;
            PlayerDetails.Instance.HairColour = data.hairColor;
            PlayerDetails.Instance.Skin = data.skin;
            PlayerDetails.Instance.Head = data.head;
            PlayerDetails.Instance.Eye = data.eyes;
            PlayerDetails.Instance.EyeColour = data.eyeColor;
            PlayerDetails.Instance.PPos = new Vector2(data.pPositionX, data.pPositionY);
            PlayerDetails.Instance.AreaPosX = data.areaX;
            PlayerDetails.Instance.AreaPosY = data.areaY;
            PlayerDetails.Instance.PName = data.pName;
            PlayerDetails.Instance.FromPortal = data.fromPortal;
            PlayerDetails.Instance.PortalID = data.portalID;
            LocationsManager.Instance.MapContents = new List<MapContents>();
            LocationsManager.Instance.MapContents = data.mapContents;
            PlayerDetails.Instance.NewGame = true;
            PlayerDetails.Instance.Loaded = true;
        }

        private static void DoDelete(StorageDevice device, string fileName)
        {
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Add the container path to our file name.
            string Filename = fileName + ".sav";

            if (container.FileExists(Filename))
            {
                container.DeleteFile(Filename);
            }

            // Dispose the container, to commit the change.
            container.Dispose();
            deleted = true;
        }

    }
}
