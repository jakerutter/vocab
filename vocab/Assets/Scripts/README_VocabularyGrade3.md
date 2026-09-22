# Vocabulary Game – Grade 3 Starter Pack

## Unity locations

Copy the files into your Unity project like this:

    Assets/
      Resources/
        Vocabulary/
          grade3.json
      Scripts/
        Vocabulary/
          VocabularyModels.cs
          VocabularyLoader.cs

Unity's `JsonUtility` can deserialize this JSON because `VocabularyGradeJson`
and `VocabularyEntryJson` have public serializable fields with matching names.
A JSON dictionary object (`{"word": "definition"}`) isn't used because
JsonUtility does not support deserializing Dictionary<TKey,TValue>.

## Loading Grade 3

    List<VocabularyWord> words = VocabularyLoader.LoadGrade(3);
    Dictionary<string, VocabularyWord> lookup = VocabularyLoader.BuildLookup(words);
    string definition = lookup["ABSORB"].Definition;

## Loading for a game session

    int selectedGrade = 3;
    bool includeLowerGrades = false;
    List<VocabularyWord> eligible = VocabularyLoader.LoadEligibleWords(
        selectedGrade, includeLowerGrades);

    var settings = new GameSettings
    {
        Grade = selectedGrade,
        IncludeLowerGrades = includeLowerGrades
    };
    var game = new VocabularyGame();  // From the earlier game-engine script.
    game.StartGame(eligible, settings);

Note: only grade3.json is supplied here. Selecting grade 3 with
includeLowerGrades=true will require grade1.json and grade2.json to exist too.
Missing grade files throw an informative exception rather than silently
excluding grades. Each future file uses the same JSON schema.

## Content conventions

- `partOfSpeech` is the primary part of speech of the supplied definition.
  Some words have other meanings or parts of speech; expand to multiple
  entries/senses later if needed.
- `relatedWords` are curated contextual associations, not verified synonyms
  or guaranteed suitable multiple-choice distractors. Some are not present
  in Grade 3. Filter against eligible vocabulary and avoid ambiguous answers
  before using them to build answer options.
- `synonyms` and `antonyms` are intentionally empty arrays.
- Definitions preserve the originally supplied Grade 3 text unchanged.
- If a word appears in multiple selected grades, LoadEligibleWords selects
  its highest-grade entry for that session.
- `Resources` assets are packaged at build time. Editing JSON after a WebGL
  build is deployed requires rebuilding, unless the game later uses an
  external data service instead.
