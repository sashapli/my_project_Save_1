
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int Coin;
        public int LevalInt1 = 0;
        public int LevalInt2 = 0;
        public int LevalInt3 = 0;
        public float time = 0;
        public float VoulemGameSound = 0.5f;
        public float VoulemMusic = 1;
        public bool IsMove = false;
    }
}
