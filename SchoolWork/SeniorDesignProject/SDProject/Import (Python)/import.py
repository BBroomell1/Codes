import sqlite3, csv
import pandas as pd
import tkinter as tk
from tkinter import filedialog

# get file path from user
root = tk.Tk()
root.withdraw()
file_path = filedialog.askopenfilename() 

# if file is xlxs type convert to csv
if file_path.lower().endswith(".xlsx"):
    path = input("File must be CSV type. Now converting file to CSV. \nPlease enter new file name: ") + ".csv"
    data_xls = pd.read_excel(file_path, index_col=None)
    data_xls.to_csv(path, encoding='utf-8', index=False)
    file_path = path

# load data
try:
    df = pd.read_csv(file_path)
except OSError:
    print("Could not load file")

# strip whitespace from headers
df.columns = df.columns.str.strip()

con = sqlite3.connect(":memory:")

# drop data into database
df.to_sql("MyTable", con)

# create cursor for table
cursor = con.cursor()

# execute basic lookup query
query = """ SELECT DISTINCT * FROM MyTable WHERE ID = '223564378' """
query2 = """ SELECT DISTINCT COUNT(*) FROM MyTable WHERE ID = '223564378' """
cursor.execute(query)
res = cursor.fetchall()
cursor.execute(query2)
res2 = cursor.fetchone()[0]

print(str(res2) + " occurences found \n")

for row in res:
            print("Name: ", row[1]) 
            print("ID: ", row[2])
            print("Race: ", row[3])
            print("Rank: ", row[4])
            print("Role: ", row[5])
            print("Training: ", row[6])
            print("Course: ", row[7])
            print("Iteration: ", row[8])
            print("Started: ", row[9])
            print("Ended: ", row[10])
            print("InStat: ", row[11])
            print("Outstat: ", row[12])
            print("Outreason: ", row[13])
            print("\n")

con.close()
