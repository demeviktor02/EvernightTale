[System.Serializable]
public class SettingMenuData
{
    public float volume;
    public int graphics;
    public bool isFullScreen;
    public int resolution;

    public SettingMenuData(SettingsMenu settingMenu)
    {
        volume = settingMenu.volumeData;
        graphics = settingMenu.graphicsData;
        isFullScreen = settingMenu.isFullScreenData;
        resolution = settingMenu.resolutionData;
    }
}
