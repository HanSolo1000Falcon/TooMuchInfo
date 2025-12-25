using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;

namespace TooMuchInfo;

[BepInPlugin(Constants.PluginGuid, Constants.PluginName, Constants.PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string GorillaInfoEndPointURL =
            "https://raw.githubusercontent.com/HanSolo1000Falcon/GorillaInfo/main/";

    public static Dictionary<string, string> KnownMods;
    public static Dictionary<string, string> KnownCheats;
    public static Dictionary<string, string> KnownPeople;
    public static Dictionary<string, string> KnownCosmetics;

    private void Awake() => new Harmony(Constants.PluginGuid).PatchAll();

    private void Start() => GorillaTagger.OnPlayerSpawned(() =>
                                                          {
                                                              using HttpClient httpClient = new();
                                                              HttpResponseMessage gorillaInfoEndPointResponse =
                                                                      httpClient.GetAsync(
                                                                              GorillaInfoEndPointURL +
                                                                              "KnownCheats.txt").Result;

                                                              using (Stream stream = gorillaInfoEndPointResponse.Content
                                                                            .ReadAsStreamAsync().Result)
                                                              {
                                                                  using (StreamReader reader = new(stream))
                                                                  {
                                                                      KnownCheats =
                                                                              JsonConvert
                                                                                     .DeserializeObject<
                                                                                              Dictionary<string,
                                                                                                      string>>(reader
                                                                                             .ReadToEnd());
                                                                  }
                                                              }

                                                              HttpResponseMessage knownModsEndPointResponse =
                                                                      httpClient.GetAsync(
                                                                                      GorillaInfoEndPointURL +
                                                                                      "KnownMods.txt")
                                                                             .Result;

                                                              using (Stream stream = knownModsEndPointResponse.Content
                                                                            .ReadAsStreamAsync().Result)
                                                              {
                                                                  using (StreamReader reader = new(stream))
                                                                  {
                                                                      KnownMods = JsonConvert
                                                                             .DeserializeObject<
                                                                                      Dictionary<string, string>>(
                                                                                      reader.ReadToEnd());
                                                                  }
                                                              }

                                                              HttpResponseMessage knownPeopleEndPointResponse =
                                                                      httpClient.GetAsync(
                                                                                      GorillaInfoEndPointURL +
                                                                                      "KnownPeople.txt")
                                                                             .Result;

                                                              using (Stream stream = knownPeopleEndPointResponse.Content
                                                                            .ReadAsStreamAsync().Result)
                                                              {
                                                                  using (StreamReader reader = new(stream))
                                                                  {
                                                                      KnownPeople = JsonConvert
                                                                             .DeserializeObject<
                                                                                      Dictionary<string, string>>(
                                                                                      reader.ReadToEnd());
                                                                  }
                                                              }

                                                              HttpResponseMessage knownCosmeticsEndPointResponse =
                                                                      httpClient.GetAsync(
                                                                                      GorillaInfoEndPointURL +
                                                                                      "KnownCosmetics.txt")
                                                                             .Result;

                                                              using (Stream stream = knownCosmeticsEndPointResponse
                                                                            .Content
                                                                            .ReadAsStreamAsync().Result)
                                                              {
                                                                  using (StreamReader reader = new(stream))
                                                                  {
                                                                      KnownCosmetics = JsonConvert
                                                                             .DeserializeObject<
                                                                                      Dictionary<string, string>>(
                                                                                      reader.ReadToEnd());
                                                                  }
                                                              }
                                                          });
}