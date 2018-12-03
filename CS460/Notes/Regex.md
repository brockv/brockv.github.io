# **Useful Regular Expressions**


### **Date in DD/MM/YYYY Format**

/^(0?[1-9]|[12][0-9]|3[01])([ \/\-])(0?[1-9]|1[012])\2([0-9][0-9][0-9][0-9])(([ -])([0-1]?[0-9]|2[0-3]):[0-5]?[0-9]:[0-5]?[0-9])?$/

### **Time in 24-Hour Format**

/^([01]?[0-9]|2[0-3]):[0-5][0-9]$/

### **Positive Integer**

/^\d+$/

### **Negative Integer**

/^-\d+$/

### **Integer**

/^-?\d+$/

### **Positive Number**

/^\d*\.?\d+$/

### **Negative Number**

/^-\d*\.?\d+$/

### **Positive or Negative Number**

/^-?\d*\.?\d+$/

### **Number in $XXX,XXX.XX Currency Format**

/\d(?=(\d{3})+\.)/g