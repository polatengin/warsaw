FROM mcr.microsoft.com/dotnet/sdk:5.0

# Avoid warnings by switching to noninteractive
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get -y install --no-install-recommends apt-utils dialog libicu63 libssl1.1 2>&1 \