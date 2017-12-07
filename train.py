import pandas as pd                                                 
train=pd.read_json('trainData.json',lines=True,encoding='utf-8')                                                                     
station=[]
for index,row in train.iterrows():                                                                                                     
    data=row["data"]["data"]                                                                                                           
    if len(data)==0:                                                                                                                   
        continue                                                                                                                       
    station.append(data[0])                                                                                                            
    for item in data[1:]:                                                                                                              
        item["end_station_name"]=data[0]["end_station_name"]                                                                           
        item["service_type"]=data[0]["service_type"]                                                                                   
        item["start_station_name"]=data[0]["start_station_name"]                                                                       
        item["station_train_code"]=data[0]["station_train_code"]                                                                       
        item["train_class_name"]=data[0]["train_class_name"]                                                                           
        station.append(item)                                                                                                                                                                                                              
trainStation=pd.DataFrame(station)                                                                                               
trainStation.to_csv("train_station.csv",index=False)                                                                                                                                     
from sqlalchemy import create_engine                                                                                                   
engine=create_engine("sqlite:///PaperProject.db")                                                                                      
trainStation.to_sql("TrainStation",engine,index=False,if_exists'replace')                                                              
trainStation.to_sql("TrainStation",engine,index=False,if_exists='replace')                                                             