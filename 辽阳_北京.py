import json
from datetime import datetime
from sqlalchemy import create_engine
import pandas as pd

engine=create_engine("sqlite:///PaperProject.db")

data=json.load(open("./辽阳_换乘_北京.json",encoding="utf-8",mode='r'))

train_list=[line for line in data["data"]["middleList"]]
search_line=[]
for line in train_list:
    item=[line["from_station_name"],line["fullList"][0]["station_train_code"],line["middle_station_name"],line["fullList"][1]["station_train_code"],line["end_station_name"],line["all_lishi"],line["wait_time"],0,line["all_lishi_minutes"],line["wait_time_minutes"]]
    search_line.append(item)
df_all=pd.DataFrame(search_line,columns=["from_station_name","from_train_code","change_station_name","change_train_code","to_station_name","all_time","change_time","price","all_minutes","change_minutes"])
df_all.Id=df_all.index+1
                                                                                                                     
df_all["Id"]=df_all.index+1
df_all.to_sql("SearchLine1",engine,if_exists="replace",index=False)
