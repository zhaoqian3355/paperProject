from bs4 import BeautifulSoup
import pandas as pd
from sqlalchemy import create_engine

data=[]
with open("./ship_html.txt",mode='r',encoding="utf-8") as f:
    soup=BeautifulSoup(f.read())
    rows=soup.find_all("li")
    for row in rows:
        item=[]
        for i in row.find_all("i")[:-1]:
            item.append(i.text)
        data.append(item)
        

ship=pd.DataFrame(data,columns=["Id","ship_name","from_dock_name","from_time","to_dock_name","ship_line"])
ship.Id=ship.index+1
obj=ship.ship_line.str.split("-").tolist()
df=pd.DataFrame(obj,columns=["from_city","to_city"])
df.to_city=df.to_city.str.replace("大连滚装船","大连")
df["to_city"]=df.to_city.replace("烟台滚装船","烟台")
df["to_city"]=df.to_city.replace("烟台高速船","烟台")
df["to_city"]=df.to_city.replace("烟台高速轮","烟台")
ship["from_city"]=df.from_city
ship["to_city"]=df.to_city
engine=create_engine("sqlite:///PaperProject.db")
ship.to_sql("Ship",engine,if_exists="replace",index=False)