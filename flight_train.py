import json
from datetime import datetime
from sqlalchemy import create_engine
import pandas as pd

engine=create_engine("sqlite:///PaperProject.db")
data=json.load(open("./flight_train.json",encoding="utf-8",mode="r"))
lineData=[]
for item in data["fis"]:
    for train in item["axpList"]:
        temp=[]
        temp.append(item["dpbn"])
        temp.append(item["fn"])
        temp.append(item["apbn"]+"-"+train["fs"]["sn"])
        temp.append(train["num"])
        temp.append(train["ts"]["sn"])
        t1=datetime.strptime(train["tt"],"%Y-%m-%d %H:%M:%S")-datetime.strptime(item["at"],"%Y-%m-%d %H:%M:%S")
        temp.append(str(t1.seconds//3600)+"时"+str((t1.seconds-t1.seconds//3600*3600)//60)+"分")
        t2=datetime.strptime(train["ft"],"%Y-%m-%d %H:%M:%S")-datetime.strptime(item["at"],"%Y-%m-%d %H:%M:%S")
        temp.append(str(t2.seconds//3600)+"时"+str((t2.seconds-t2.seconds//3600*3600)//60)+"分")
        temp.append(item["lp"])
        lineData.append(temp)

df=pd.DataFrame(lineData,columns=["from_station_name","from_train_code","change_station_name","change_train_code","to_station_name","all_time","change_time","price"])
df["Id"]=df.index+1

data=json.load(open("./train_change_line.json",encoding="utf-8",mode='r'))

train_list=[line for item in data for line in item["data"]["middleList"]]
search_line=[]
for line in train_list:
    item=[line["from_station_name"],line["fullList"][0]["station_train_code"],line["middle_station_name"],line["fullList"][1]["station_train_code"],line["end_station_name"],line["all_lishi"],line["wait_time"],0]
    search_line.append(item)
df_train=pd.DataFrame(search_line,columns=["from_station_name","from_train_code","change_station_name","change_train_code","to_station_name","all_time","change_time","price"])
df_all=pd.concat([df,df_train],ignore_index=True)
df_all.Id=df_all.index+1
df_all.to_sql("SearchLine",engine,if_exists="replace",index=False)