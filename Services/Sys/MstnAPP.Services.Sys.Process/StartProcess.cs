namespace MstnAPP.Services.Sys.Process
{
    public class StartProcess
    {
        public static void OpenGithub()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "https://github.com/xqyjlj/misaka station");
        }

        public static void OpenEmail()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "mailto: xqyjlj@126.com");
        }

        public static void OpenBlog()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "https://xqyjlj.github.io");
        }

        public static void OpenDonate()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "https://xqyjlj.github.io/donate");
        }

        public static void OpenGithubReleases()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "https://github.com/xqyjlj/misaka station/releases");
        }

        public static void OpenGithubReleasesNote()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "https://github.com/xqyjlj/misaka station/releases");
        }

        public static void OpenAbout()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", "https://xqyjlj.github.io/2021/09/10/%E3%80%90%E5%BE%A1%E5%9D%82%E7%BD%91%E7%BB%9C%E3%80%91misaka station");
        }
    }
}