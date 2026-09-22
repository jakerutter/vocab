// Example how to use the VocabularyLoader to load word collections and fetch specific words
var words = VocabularyLoader.LoadGrade(3);

// Create a case-insensitive dictionary.
var lookup = VocabularyLoader.BuildLookup(words);

// Retrieve a word and its information.
VocabularyWord absorb = lookup["absorb"];

string definition = absorb.Definition;
string partOfSpeech = absorb.PartOfSpeech;

List<string> relatedWords = absorb.RelatedWords;