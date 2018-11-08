/* Create an array of words we don't want translated into GIFS. */
var boringWords =
    ["I", "the", "be", "to", "of", "and", "a", "in", "that", "have", "it", "we", "say", "him", "her", "or", "an", "will", "me", "again",
        "for", "not", "on", "with", "he", "she", "as", "you", "do", "at", "this", "one", "all", "would", "there", "their", "what", "when",
        "but", "his", "hers", "by", "from", "they", "so", "up", "out", "if", "about", "who", "get", "which", "go", "me", "public", "are",
        "make", "can", "like", "time", "no", "just", "know", "take", "people", "into", "year", "your", "good", "some", "could", "always", "went",
        "them", "see", "other", "than", "then", "now", "look", "only", "come", "its", "it's", "over", "think", "also", "back", "really", "store",
        "after", "use", "two", "how", "our", "work", "first", "well", "way", "even", "new", "want", "because", "any", "these", "give", "every",
        "day", "most", "us", "my", "mine", "yours", "they're", "I'm", "going", "took", "huge", "big", "large", "very", "am", "let's", "had",
        "try", "wicked", "pretty", "is", "please", "was", "name", "named", "stay", "away", "any", "anymore", "we're", "brought", "does", "did"];


/**
 * Helper function to get the length of the entire string in the input field.
 */
function userInputLength() {

    /* Return the length of the input */
    return $("#userInput").val().length;
}

/**
 * Helper function to compare the word to our list of "fun" words.
 * @param {any} lastWord The last word typed into the input field.
 */
function isFunWord(lastWord) {

    /* Convert the word to lower case for comparisons */
    var temp = lastWord;

    /* Check if this word is in our list of "fun" words */
    var isFunWord = true;
    for (var i = 0; i < boringWords.length; i++) {
        if (boringWords[i].toLowerCase() == temp.toLowerCase()) {
            isFunWord = false;
        }
    }

    /* Return true if it was in our list, false otherwise */
    return isFunWord;
}

/**
 * This function extracts the image URL from the JSON object and uses it
 * to insert the gif into the view.
 * @param {any} result The JSON object sent from the controller.
 */
function insertGIF(result) {

    /* Grab the image type we want to display */
    var giphyURL = result.data.images.preview_gif.url;

    /* DEBUG -- Write the URL to console */
    console.log(giphyURL)

    /* Add the gif we grabbed to the "container" <div> tag using its id (#) */
    $("#messageContainer").append("<img width='100px;' height='100px;' src='" + giphyURL + "'/>");
}

/**
 * This function inserts the word passed in directly to the view.
 * @param {any} lastWord The word to insert in the view.
 */
function insertWord(lastWord) {

    /* Add the word entered to the "container" <div> tag using its id (#) */
    $("#messageContainer").append(" " + "<label> " + lastWord + " </label>");
}

/**
 * This function gets the last word typed into the input field.
 */
function getLastWordTyped() {    

    /* Initialize a variable to hold the word we're after */
    var lastWord = "";

    /* Get the text from the input field */
    var input = document.getElementById("userInput").value;

    /* Trim the any punctuation, as well as the last space from the string */
    var parsedInput = input.substr(0, input.length).replace(/[.,\/#!$%\^&\*;:{}=\-_`~()]/g, "");

    /* Don't do anything if the string is just spaces */
    if (/\S/.test(parsedInput)) {

        /* Grab the last word from the string */
        lastWord = parsedInput.split(" ").pop();    
    }

    /* Return the word to be used */
    return lastWord;
}

/**
 * This function determins what to do with the word we grabbed.
 * @param {any} lastWord The last word typed into the input field.
 */
function chooseAction(lastWord) {

    /* If this is a word we're interested in translating, do GIPHY magic */
    if (isFunWord(lastWord)) {

        $.ajax({

            dataType: "json",
            url: "/Main/TranslateGIF?=",
            data: { "lastWord": lastWord },
            success: insertGIF
        });
    }
    /* It's a "boring" word -- just send it to the view */
    else {

        insertWord(lastWord);
    }
}

///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() {

    /* Give the input focus */
    $("#userInput").focus();

    /* Callback function that listens for the spacebar to be pressed */
    $("#userInput").keypress(function () {

        /* 32 == spacebar -- Was it pressed? */
        if ($("#userInput").val().length > 0 && event.which === 32) {

            /* Get the word the client typed before this space */
            var lastWord = getLastWordTyped();

            /* Determine what to do with the word */
            chooseAction(lastWord);

            /* Give the input focus again for ease of use */
            $("#userInput").focus();
        }
    });
}

/* Call main() once the page is fully loaded */
$(document).ready(main);