/*eslint-env browser*/
/*jslint browser: true*/
/*global $*/

var wordsToTranslate = ['walk', 'walking', 'jumping', 'climbing', 'running', 'sprinting',
    'lobster', 'cat', 'dog', 'bear', 'bird', 'turtle', 'ferret', 'eagle'];


/* Call main() once the page is fully loaded */
$(document).ready(main);

/**
  * Grab the length string from the input field
  */
function userInputLength() {
    /* Enabled to shut the editor up */
    "use strict";

    /* Return the length of the input */
    return $("#userInput").val().length;
}

function isFunWord(lastWord) {
    /* Check if this is a word we want to translate */
    var isFunWord = false;
    for (var i = 0; i < wordsToTranslate.length; i++) {
        if (wordsToTranslate[i] == lastWord) {
            isFunWord = true;
        }
    }

    return isFunWord;
}

function insertGIF(result) {

    /* Initialize a string for inserting the gif */
    var output = "";

    /* Grab just the image type we want to display */
    var giphyURL = result.data.images.preview_gif.url;

    /* Write the URL to console */
    console.log(giphyURL)

    /* Add the gif we grabbed to the "container" <div> tag using its id (#) */
    output += " <img width='200px' height='200px' src='" + giphyURL + "'/>";
    $("#container").append(output);
}

/**
 * This function gets the last word the user typed into the input field
 * after presses the spacebar.
 */
function getLastWordTyped() {    
    /* Get the text from the input field */
    var input = document.getElementById("userInput").value;

    /* Trim the last space from the string */
    var parsedInput = input.substr(0, input.length);

    /* Don't do anything if the string is just spaces */
    if (/\S/.test(parsedInput)) {

        /* Grab the last word from the string */
        var lastWord = parsedInput.split(" ").pop();

        /* If this is a word we're interested in translating, do GIPHY magic */
        if (isFunWord(lastWord)) {            
            $.ajax({
                dataType: "json",
                url: "/Main/TranslateGIF?=",
                data: { "lastWord": lastWord },
                success: insertGIF
            });
        }
        else {
            /* Add the word entered to the "container" <div> tag using its id (#) */
            var output = "<label> " + lastWord + " </label>";
            $("#container").append(" " + output);
        }
    }
}

///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() {    
    /* Callback function that grabs the last word entered into the input after the spacebar is pressed */
    $("#userInput").keypress(function () {

        /* 32 == spacebar -- Was it pressed? */
        if (userInputLength() > 0 && event.which === 32) {
            getLastWordTyped();
        }
    });
}