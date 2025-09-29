window.multiselect = function () {

     console.log("salom")
     const dropdownButton = document.getElementById("dropdownButton");
     const dropdownContent = document.getElementById("dropdownContent");

     function toggleDropdown(button, content) {
          content.classList.toggle("show");
     }

     if (dropdownButton && dropdownContent) {
          dropdownButton.addEventListener("click", () =>
               toggleDropdown(dropdownButton, dropdownContent)
          );
     }

     const selectDropdownButton = document.getElementById("selectDropdownButton");
     const selectDropdownContent = document.getElementById("selectDropdownContent");
     const selectedValueSpan = document.getElementById("selectedValue");
     const hiddenSelectValueInput = document.getElementById("hiddenSelectValue");
     const selectOptions = selectDropdownContent?.querySelectorAll(".select-option");

     if (selectDropdownButton && selectDropdownContent) {
          selectDropdownButton.addEventListener("click", () =>
               toggleDropdown(selectDropdownButton, selectDropdownContent)
          );
     }

     if (selectOptions) {
          selectOptions.forEach((option) => {
               option.addEventListener("click", function (event) {
                    event.preventDefault();
                    const value = this.getAttribute("data-value");
                    const text = this.textContent;

                    if (selectedValueSpan) selectedValueSpan.textContent = text;
                    if (hiddenSelectValueInput) hiddenSelectValueInput.value = value;

                    selectDropdownContent.classList.remove("show");
               });
          });
     }

     // Multi-select qismi
     const multiSelectDropdownButton = document.getElementById("multiSelectDropdownButton");
     const multiSelectDropdownContent = document.getElementById("multiSelectDropdownContent");
     const selectedMultiValuesSpan = document.getElementById("selectedMultiValues");
     const hiddenMultiSelectValueInput = document.getElementById("hiddenMultiSelectValue");
     const multiSelectOptions = multiSelectDropdownContent?.querySelectorAll(".multi-select-option");

     let selectedMultiValues = [];

     function updateMultiSelectDisplay() {
          if (!selectedMultiValuesSpan) return;

          selectedMultiValuesSpan.innerHTML = "";
          if (selectedMultiValues.length === 0) {
               const placeholder = document.createElement("span");
               placeholder.classList.add("placeholder");
               placeholder.textContent = "Choose";
               selectedMultiValuesSpan.appendChild(placeholder);
          } else {
               selectedMultiValues.forEach((value) => {
                    const optionElement = Array.from(multiSelectOptions).find(
                         (opt) => opt.getAttribute("data-value") === value
                    );
                    if (optionElement) {
                         const tag = document.createElement("span");
                         tag.classList.add("selected-tag");
                         tag.textContent = optionElement.textContent.trim();
                         selectedMultiValuesSpan.appendChild(tag);
                    }
               });
          }
          if (hiddenMultiSelectValueInput)
               hiddenMultiSelectValueInput.value = selectedMultiValues.join(",");
     }

     if (multiSelectDropdownButton && multiSelectDropdownContent) {
          multiSelectDropdownButton.addEventListener("click", () =>
               toggleDropdown(multiSelectDropdownButton, multiSelectDropdownContent)
          );
     }

     if (multiSelectOptions) {
          multiSelectOptions.forEach((option) => {
               option.addEventListener("click", function (event) {
                    event.preventDefault();
                    const value = this.getAttribute("data-value");
                    const checkboxIcon = this.querySelector(".checkbox-icon svg");

                    if (this.classList.contains("selected")) {
                         this.classList.remove("selected");
                         if (checkboxIcon) checkboxIcon.style.display = "none";
                         selectedMultiValues = selectedMultiValues.filter((v) => v !== value);
                    } else {
                         this.classList.add("selected");
                         if (checkboxIcon) checkboxIcon.style.display = "block";
                         selectedMultiValues.push(value);
                    }
                    updateMultiSelectDisplay();
               });
          });
     }

     updateMultiSelectDisplay();

     // tashqariga bosilganda yopish
     window.addEventListener("click", function (event) {
          if (
               dropdownButton &&
               !dropdownButton.contains(event.target) &&
               dropdownContent.classList.contains("show")
          ) {
               dropdownContent.classList.remove("show");
          }
          if (
               selectDropdownButton &&
               !selectDropdownButton.contains(event.target) &&
               selectDropdownContent.classList.contains("show")
          ) {
               selectDropdownContent.classList.remove("show");
          }
          if (
               multiSelectDropdownButton &&
               !multiSelectDropdownButton.contains(event.target) &&
               multiSelectDropdownContent.classList.contains("show")
          ) {
               multiSelectDropdownContent.classList.remove("show");
          }
     });

     // ESC tugmasi bilan yopish
     window.addEventListener("keydown", function (event) {
          if (event.key === "Escape") {
               if (dropdownContent.classList.contains("show")) {
                    dropdownContent.classList.remove("show");
               }
               if (selectDropdownContent.classList.contains("show")) {
                    selectDropdownContent.classList.remove("show");
               }
               if (multiSelectDropdownContent.classList.contains("show")) {
                    multiSelectDropdownContent.classList.remove("show");
               }
          }
     });
}
