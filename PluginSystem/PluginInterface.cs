// PluginInterface.cs
using System;

namespace NekoBeats.Plugins
{
    public interface INekoBeatsPlugin
    {
        string Name { get; }
        string Version { get; }
        string Author { get; }
        
        void Initialize(INekoBeatsHost host);
        void OnEnable();
        void OnDisable();
        void OnUpdate(float deltaTime);
        void Dispose();
    }

    public interface INekoBeatsHost
    {
        void Log(string message);
        void SetBarColor(int argb);
        void SetOpacity(float opacity);
        void SetBarHeight(int height);
        void SetBarCount(int count);
        void SetCustomBackground(string imagePath);
        void ClearCustomBackground();
        void ApplyGradient(int[] colorArgbs);
        void SetLatencyCompensation(int milliseconds);
        void SetFadeEffect(bool enabled, float fadeSpeed);
        float GetAudioLevel();
        int GetCurrentFPS();
    }
}
