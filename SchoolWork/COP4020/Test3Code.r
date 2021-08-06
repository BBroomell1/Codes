#QuesTION 1
#read data from test3data.csv
data1 <- read.csv("test3data.csv", na.strings=c(""))
data1 <- data1[!is.na(data1$Industry),]
rownames(data1) <- 1:nrow(data1)

#change to character from factor
data1$State <- as.character(data1$State) 
data1$City <- as.character(data1$City)

#change revenue and expenses to numeric
data1$Revenue <- gsub("\\$", "", data1$Revenue)
data1$Revenue <- gsub(",", "", data1$Revenue)
data1$Revenue <- gsub("Dollars", "", data1$Revenue)
data1$Revenue <- as.numeric(as.character(data1$Revenue))

data1$Expenses <- gsub("\\$", "", data1$Expenses)
data1$Expenses <- gsub(",", "", data1$Expenses)
data1$Expenses <- as.numeric(as.character(data1$Expenses))

#fill in states
data1[is.na(data1$State) & data1$City =="Jackson", "State"] <- "Mississippi"
data1[is.na(data1$State) & data1$City =="Cheyenne", "State"] <- "Wyoming"
data1[is.na(data1$State) & data1$City =="Sacramento", "State"] <- "California"

#fill in cities - no missing cities

#fill in NA for Revenue, Profit, Expenses
meanRevenue <- mean(data1$Revenue, na.rm=TRUE)
data1[is.na(data1$Revenue),"Revenue"] <- meanRevenue

meanExpenses <- mean(data1$Expenses, na.rm=TRUE)
data1[is.na(data1$Expenses),"Expenses"] <- meanExpenses

meanProfit <- mean(data1$Profit, na.rm=TRUE)
data1[is.na(data1$Profit), "Profit"] <- meanProfit

#median of profit column where month is 6
medianProfit <- median(subset(data1$Profit,data1$Month==6))

#QUESTION 2
#read data from cherry.csv
data2 <- read.csv("cherry.csv")
data2 <- data2[ , -which(names(data2) %in% c("Diam"))]
data2 <- t(as.matrix(data2))

#make a bar plot
barplot(data2, main = "Graph",
        col = c("blue", "red"),
        legend.text = c("Height", "Volume"),
        beside = TRUE, xlab = "ID",name = c(1:31))
 
#QUESTION 3           
#Read in data from cherry.csv           
data3 <- read.csv("cherry.csv")

#plot data
plot(data3$Height, data3$Volume, col = c("blue", "red"), 
     ylab = "Volume", xlab = "Height",
     main = "Graph Height VS Volume")

#linear regression
linre <- lm(data3$Volume ~ data3$Height)
abline(linre)
