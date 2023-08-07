FROM python:3.9
MAINTAINER "ikjunjang@nextk.co.kr"

#
# TimeZone 서울로 설정
#
ENV TZ Asia/Seoul

#
# TimeZone 서울로 설정
#
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

RUN pip install pyzmq && \
    pip install python-dateutil && \
    pip install flask && \
    pip install requests && \
    pip install flask_cors

RUN mkdir /home/ai_edge_link

#
# 필요 파일들 copy
#
COPY . /home/ai_edge_link

WORKDIR /home/ai_edge_link

CMD ["python", "./main.py"]