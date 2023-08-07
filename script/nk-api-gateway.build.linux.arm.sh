if [ $# -eq 1 ]
then
    SOLUTION_NAME=nk-api-gateway
    APP_NAME=surv-api
    ARCH=arm
    REPO=192.168.0.70:5000
    VERSION_TAG=$1

    #
    # 베이스 기반 빌드
    #
    docker build -f "dockerfiles/Dockerfile" -t $SOLUTION_NAME/$APP_NAME/$ARCH:$VERSION_TAG .
    docker tag $SOLUTION_NAME/$APP_NAME/$ARCH:$VERSION_TAG $REPO/$SOLUTION_NAME/$APP_NAME/$ARCH:$VERSION_TAG
    docker push $REPO/$SOLUTION_NAME/$APP_NAME/$ARCH:$VERSION_TAG

    else
    echo "sh nk-api-gateway.build.linux.arm.sh [1.0.0]"
fi

