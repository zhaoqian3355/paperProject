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
        t1=datetime.strptime(train["tt"],"%Y-%m-%d %H:%M:%S")-datetime.strptime(item["dt"],"%Y-%m-%d %H:%M:%S")
        temp.append(str(t1.seconds//3600)+"时"+str((t1.seconds-t1.seconds//3600*3600)//60)+"分")
        t2=datetime.strptime(train["ft"],"%Y-%m-%d %H:%M:%S")-datetime.strptime(item["at"],"%Y-%m-%d %H:%M:%S")
        temp.append(str(t2.seconds//3600)+"时"+str((t2.seconds-t2.seconds//3600*3600)//60)+"分")
        temp.append(item["lp"])
        lineData.append(temp)

df=pd.DataFrame(lineData,columns=["from_station_name","from_train_code","change_station_name","change_train_code","to_station_name","all_time","change_time","price"])
df=df.drop_duplicates()
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

df_all.all_time=df_all.all_time.str.replace("小时","时")
df_all.all_time=df_all.all_time.str.replace("分钟","分")                                                                                                               
df_all.change_time=df_all.change_time.str.replace("小时","时")
df_all.change_time=df_all.change_time.str.replace("分钟","分")                                                                                                                                    
all_hours=df_all.all_time.str.split("时").tolist()                                                                                                                      
times=pd.DataFrame(all_hours,columns=["hour","minute"]);                                                                                                                    
times.minute=times.minute.str.replace("分","")                                                                                                                             
times.minute=times.minute.str.replace("小","")                                                                                                                               
times.minute=times.minute.str.replace("钟","")                                                                                                                                                       
times.hour=times.hour.apply(int)                                                                                                                                          
times.minute=times.minute.apply(int)                                                                                                                                                            
times.all_minutes=0                                                                                                                                                            
times.all_minutes=times.hour*60+times.minute                                                                                                                                      
df_all["all_minutes"]=times.all_minutes                                                                                                                                  
changes=pd.DataFrame(df_all.change_time.str.split("时").tolist(),columns=list('hm'))                                                                                                                                                         
changes.m[38]=changes.h[38]                                                                                                                                                                
changes.h[38]=0                                                                                                                                                               
changes.h=changes.h.apply(int)                                                                                                                                            
changes.m=changes.m.str.replace("分","")                                                                                                                                   
changes.m=changes.m.str.replace("钟","")                                                                                                                                   
changes.m=changes.m.apply(int)                                                                                                                                                 
df_all["change_minutes"]=changes.h*60+changes.m                                                                                                                           

df_all.to_sql("SearchLine",engine,if_exists="replace",index=False)
