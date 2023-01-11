using UnityEditor;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class CancelInactivePlatformBuild
    {
        private const string TITLE = "Build Cancel";

        private const string MESSAGE = @"Build Settings や Project Settings で
アクティブではない {0} プラットフォームが
選択されていたのでビルドをキャンセルしました";

        static CancelInactivePlatformBuild()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler( OnBuild );
        }

        private static void OnBuild( BuildPlayerOptions options )
        {
            var target = options.target;

            if ( EditorUserBuildSettings.activeBuildTarget != target )
            {
                var message = string.Format( MESSAGE, target.ToString() );

                EditorUtility.DisplayDialog( TITLE, message, "OK" );

                return;
            }

            BuildPipeline.BuildPlayer( options );
        }
    }
}