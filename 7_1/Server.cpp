#include<WinSock2.h>
#include<stdio.h>
#include <stdlib.h> 

#pragma comment(lib,"ws2_32")

#define PORT 4658
#define PACKTE_SIZE 1024

int main() {
	//������ ���� �ʱ�ȭ ������ �����ϴ� ����ü
	WSADATA wsaData;
	int ret = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (ret != 0) {
		printf("WSAStartup failed - Error Code: %d \n", ret);
		return 1;
	}

	//���� ����
	SOCKET hListen;
	hListen = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	
	//�ּ������� ����ֵδ� ����ü
	SOCKADDR_IN tListenAddr = {};
	tListenAddr.sin_family = AF_INET;
	tListenAddr.sin_port = htons(PORT);
	tListenAddr.sin_addr.s_addr = htonl(INADDR_ANY);

	//���Ͽ� �ּ������� ����
	bind(hListen, (SOCKADDR*)&tListenAddr, sizeof(tListenAddr));

	//���� �� ���¿��� ������ ���º���
	listen(hListen, SOMAXCONN);


	//Ŭ���̾�Ʈ �� ���� ���� �� ������ ���� ����ü
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