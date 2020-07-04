#include<iostream>
#include "send.h"

TW_ExchangeManager::TW_ExchangeManager()
{
	mp_data = NULL;
	m_total_size = 0;
	m_cur_size = 0;
}

TW_ExchangeManager::~TW_ExchangeManager()
{
	DeleteData();
}

char* TW_ExchangeManager::MemoryAlloc(int a_data_size)
{
	// 기존에 사용하던 메모리와 현재 필요한 메모리가 크기가 동일하다면 다시 메모리를 할당할 필요가 없다. 
	// (일정한 크기의 데이터를 지속적으로 전송하거나 수신하는 경우가 많다)
	if (m_total_size != a_data_size) {
		// 이미 할당된 메모리가 있다면 제거한다.
		if (mp_data != NULL) delete[] mp_data;
		// 시정한 크기로 메모리를 할당한다.
		mp_data = new char[a_data_size];
		// 할당된 크기를 기억한다.
		m_total_size = a_data_size;
	}
	// 작업 위치를 가장 첫 위치로 초기화한다.
	m_cur_size = 0;
	// 할당된 메모리의 시작 위치를 반환한다.
	return mp_data;
}

void TW_ExchangeManager::DeleteData()
{
	if (mp_data != NULL) {
		// 할당된 메모리를 제거하고 작업과 관련된 변수들을 초기화 한다.
		delete[] mp_data;
		mp_data = NULL;
		m_total_size = 0;
	}
}

int TW_SendManager::GetPosition(char** ap_data, int a_data_size)
{
	// 새로운 전송 위치에 대한 주소를 첫번째 인자에 저장한다.
	*ap_data = mp_data + m_cur_size;

	// 전송 크기를 계산하기 위해서 2048bytes를 더한 크기가 최대 크기보다 작은지 체크한다.
	if (m_cur_size + a_data_size < m_total_size) {
		// 최대 크기보다 작은 경우 2048bytes 만큼 전송하면 된다.
		// 다음위치를 계산할 수 있도록 전송한 총 크기를 구한다.
		m_cur_size += a_data_size;
	}
	else {
		// 2048bytes 보다 작은 경우, 실제로 남은 크기만 전송한다.
		a_data_size = m_total_size - m_cur_size;
		// 현재위치를 마지막 위치로 옮긴다. (이번이 마지막 전송임)
		m_cur_size = m_total_size;
	}
	// 계산된 전송크기를 반환한다.
	return a_data_size;
}

int TW_RecvManager::AddData(char* ap_data, int a_size)
{
	// 수신된 데이터를 기존 수신 데이터 뒤에 추가한다.
	memcpy(mp_data + m_cur_size, ap_data, a_size);
	// 총 수신 크기를 계산한다.
	m_cur_size += a_size;
	// 현재 수신된 데이터의 크기를 반환한다.
	return m_cur_size;
}