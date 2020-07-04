#pragma once

// 전송과 수신에서 공통적으로 작업할 내용을 구현한 클래스
class TW_ExchangeManager
{
protected:
	// 전송 또는 수신을 위해 할당된 메모리의 전체 크기와 현재 작업중인 크기
	int m_total_size, m_cur_size;
	// 전송 또는 수신을 위해서 할당된 메모리의 시작 주소
	char* mp_data;

public:
	TW_ExchangeManager();  // 객체 생성자
	~TW_ExchangeManager(); // 객체 파괴자

	// 전송 또는 수신에 사용할 메모리를 할당한다.
	// a_data_size에 할당할 크기를 명시하면 이 함수의 반환 값으로
	// 할당된 메모리의 주소를 반환한다.(반환된 주소는 이 클래스가 관리하고 있다)
	char* MemoryAlloc(int a_data_size);
	// 전송 또는 수신에 사용되던 메모리를 제거한다.
	void DeleteData();

	inline int GetTotalSize() { return m_total_size; } // 할당된 메모리의 크기를 반환한다.
	inline int GetCurSize() { return m_cur_size; }     // 현재 작업중인 메모리의 위치를 반환한다.
};


class TW_SendManager : public TW_ExchangeManager
{
public:
	// 현재 전송할 위치와 크기를 계산한다.
	int GetPosition(char** ap_data, int a_data_size = 2048);
	// 전송이 완료되었는지 체크한다.
	inline char IsProcessing() { return m_total_size != m_cur_size; }
};

class TW_RecvManager : public TW_ExchangeManager
{
public:
	// 수신된 데이터를 기존 수신된 데이터에 추가한다.

	int AddData(char* ap_data, int a_size);
	// 수신된 데이터를 하나로 합친 메모리의 시작 주소를 얻는다.
	inline char* GetData() { return mp_data; }
};