#include<WinSock2.h>
#include<stdio.h>
#include <stdlib.h> 

#pragma comment(lib,"ws2_32")

#define PORT 4658
#define PACKTE_SIZE 1024

int main() {
	//윈도우 소켓 초기화 정보를 저장하는 구조체
	WSADATA wsaData;
	int ret = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (ret != 0) {
		printf("WSAStartup failed - Error Code: %d \n", ret);
		return 1;
	}

	//소켓 생성
	SOCKET hListen;
	hListen = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	
	//주소정보를 담아주두는 구조체
	SOCKADDR_IN tListenAddr = {};
	tListenAddr.sin_family = AF_INET;
	tListenAddr.sin_port = htons(PORT);
	tListenAddr.sin_addr.s_addr = htonl(INADDR_ANY);

	//소켓에 주소정보를 연결
	bind(hListen, (SOCKADDR*)&tListenAddr, sizeof(tListenAddr));

	//연결 된 상태에서 소켓의 상태변경
	listen(hListen, SOMAXCONN);


	//클라이언트 측 소켓 생성 및 정보를 담을 구조체
	PSOCKADDR_IN tClntAddr = {};
	int iClntSize = sizeof(tClntAddr);
	SOCKET hClient = accept(hListen, (SOCKADDR*)&tClntAddr, &iClntSize);


	char cBuffer[PACKTE_SIZE] = {};
	recv(hClient, cBuffer, PACKTE_SIZE, 0);
	printf("Recv MSG : %s\n", cBuffer);

	char cMsg[] = "Sever Send ";
	send(hClient, cMsg, strlen(cMsg), 0);

	closesocket(hClient);
	closesocket(hListen);


	WSACleanup();
	return 0;
}