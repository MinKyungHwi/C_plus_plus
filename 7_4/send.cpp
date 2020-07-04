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
	// ������ ����ϴ� �޸𸮿� ���� �ʿ��� �޸𸮰� ũ�Ⱑ �����ϴٸ� �ٽ� �޸𸮸� �Ҵ��� �ʿ䰡 ����. 
	// (������ ũ���� �����͸� ���������� �����ϰų� �����ϴ� ��찡 ����)
	if (m_total_size != a_data_size) {
		// �̹� �Ҵ�� �޸𸮰� �ִٸ� �����Ѵ�.
		if (mp_data != NULL) delete[] mp_data;
		// ������ ũ��� �޸𸮸� �Ҵ��Ѵ�.
		mp_data = new char[a_data_size];
		// �Ҵ�� ũ�⸦ ����Ѵ�.
		m_total_size = a_data_size;
	}
	// �۾� ��ġ�� ���� ù ��ġ�� �ʱ�ȭ�Ѵ�.
	m_cur_size = 0;
	// �Ҵ�� �޸��� ���� ��ġ�� ��ȯ�Ѵ�.
	return mp_data;
}

void TW_ExchangeManager::DeleteData()
{
	if (mp_data != NULL) {
		// �Ҵ�� �޸𸮸� �����ϰ� �۾��� ���õ� �������� �ʱ�ȭ �Ѵ�.
		delete[] mp_data;
		mp_data = NULL;
		m_total_size = 0;
	}
}

int TW_SendManager::GetPosition(char** ap_data, int a_data_size)
{
	// ���ο� ���� ��ġ�� ���� �ּҸ� ù��° ���ڿ� �����Ѵ�.
	*ap_data = mp_data + m_cur_size;

	// ���� ũ�⸦ ����ϱ� ���ؼ� 2048bytes�� ���� ũ�Ⱑ �ִ� ũ�⺸�� ������ üũ�Ѵ�.
	if (m_cur_size + a_data_size < m_total_size) {
		// �ִ� ũ�⺸�� ���� ��� 2048bytes ��ŭ �����ϸ� �ȴ�.
		// ������ġ�� ����� �� �ֵ��� ������ �� ũ�⸦ ���Ѵ�.
		m_cur_size += a_data_size;
	}
	else {
		// 2048bytes ���� ���� ���, ������ ���� ũ�⸸ �����Ѵ�.
		a_data_size = m_total_size - m_cur_size;
		// ������ġ�� ������ ��ġ�� �ű��. (�̹��� ������ ������)
		m_cur_size = m_total_size;
	}
	// ���� ����ũ�⸦ ��ȯ�Ѵ�.
	return a_data_size;
}

int TW_RecvManager::AddData(char* ap_data, int a_size)
{
	// ���ŵ� �����͸� ���� ���� ������ �ڿ� �߰��Ѵ�.
	memcpy(mp_data + m_cur_size, ap_data, a_size);
	// �� ���� ũ�⸦ ����Ѵ�.
	m_cur_size += a_size;
	// ���� ���ŵ� �������� ũ�⸦ ��ȯ�Ѵ�.
	return m_cur_size;
}