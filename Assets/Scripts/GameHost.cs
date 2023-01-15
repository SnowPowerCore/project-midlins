using Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Unity;
using Microsoft.Extensions.Logging;
using UnityEngine;

[DefaultExecutionOrder(-65500)]
public class GameHost : HostManager
{
    [Tooltip("Register existing game objects on the scene. The component what represents the type must be at the top of the inspector hierarchy")]
    [SerializeField] private MonoBehaviour[] _persistentMonoBehavioursOnScene;
    
    /// <summary>
    /// MonoBehaviour Awake()
    /// </summary>
    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// MonoBehaviour Start()
    /// </summary>
    protected override void OnStart()
    {
        
    }

    protected override void ConfigureAppConfiguration(IConfigurationBuilder builder)
    {
        // optionally add configuration
        // in this example it's an addressable json file
        builder.AddUtf8JsonAddressable("Assets/Settings/appsettings.json");
    }

    protected override void ConfigureLogging(ILoggingBuilder builder)
    {
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
    }

    protected override void ConfigureMonoBehaviours(IMonoBehaviourServiceCollectionBuilder services)
    {
        // configure MonoBehaviours
        
        // add referenced components via Inspector. The actual type neither constrained nor selectable, this way the first component will be picked,
        // so the component should be put at the top
        foreach (var monoBehaviour in _persistentMonoBehavioursOnScene)
        {
            services.AddMonoBehaviourSingleton(monoBehaviour);
            DontDestroyOnLoad(monoBehaviour);

            Debug.Log($"Added persistent service {monoBehaviour.name} of type {monoBehaviour.GetType()} from the current scene");
        }
    }
}