// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Restrict name fields to letters and the punctuation commonly used in names.
document.querySelectorAll(".name-input").forEach((input) => {
    input.addEventListener("input", () => {
        input.value = input.value.replace(/[^A-Za-zÀ-ÿ' -]/g, "");
    });
});
