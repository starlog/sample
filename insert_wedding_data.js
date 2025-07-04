// MongoDB script to insert sample wedding invitation data
// Connect to MongoDB: mongosh localhost:27017/barunson
// Run this script: load("insert_wedding_data.js")

// Use the barunson database
db = db.getSiblingDB('barunson');

// Sample wedding invitation data
const weddingInvitations = [
  {
    "wedding_invitation": {
      "template": {
        "opening_effect": {
          "enabled": true,
          "lettering_effect": {
            "enabled": true,
            "color": "#d4af37",
            "position": "center"
          }
        },
        "design": {
          "template_id": "elegant",
          "frame": {
            "type": "square",
            "options": ["square", "arch", "circle"]
          },
          "photo": {
            "url": "https://example.com/elegant-photo1.jpg",
            "required": true
          },
          "colors": {
            "background": "#ffffff",
            "accent": "#d4af37"
          }
        }
      },
      "fonts": {
        "title": {
          "family": "Playfair Display",
          "color": "#2c3e50",
          "size": "L",
          "size_options": ["S", "M", "L"]
        },
        "body": {
          "family": "Noto Sans",
          "color": "black",
          "color_options": ["black", "white"],
          "size": "M",
          "size_options": ["S", "M", "L"]
        }
      },
      "content": {
        "basic_info": {
          "groom": {
            "name": "김민준",
            "max_length": 20,
            "required": true
          },
          "bride": {
            "name": "이서연",
            "max_length": 20,
            "required": true
          },
          "relationship": "사랑하는 두 사람"
        },
        "ceremony_details": {
          "date": "2024-05-18",
          "time": "14:00",
          "venue": {
            "name": "그랜드 웨딩홀",
            "address": "서울시 강남구 테헤란로 123",
            "contact": "02-1234-5678"
          }
        },
        "additional_info": {
          "parents": {
            "groom_parents": {
              "father": "김태호",
              "mother": "박미경"
            },
            "bride_parents": {
              "father": "이준호",
              "mother": "최은영"
            }
          },
          "message": "저희 두 사람의 소중한 만남에 함께 해주시어 감사합니다",
          "contact_info": {
            "phone": "010-1234-5678",
            "email": "wedding@example.com"
          }
        }
      },
      "metadata": {
        "created_date": new Date("2024-01-15T10:30:00.000Z"),
        "last_modified": new Date("2024-01-15T10:30:00.000Z"),
        "version": "1.0",
        "language": "ko"
      }
    }
  },
  {
    "wedding_invitation": {
      "template": {
        "opening_effect": {
          "enabled": false,
          "lettering_effect": {
            "enabled": true,
            "color": "#ff6b6b",
            "position": "top"
          }
        },
        "design": {
          "template_id": "modern",
          "frame": {
            "type": "arch",
            "options": ["square", "arch", "circle"]
          },
          "photo": {
            "url": "https://example.com/modern-photo2.jpg",
            "required": true
          },
          "colors": {
            "background": "#f8f9fa",
            "accent": "#ff6b6b"
          }
        }
      },
      "fonts": {
        "title": {
          "family": "Montserrat",
          "color": "#495057",
          "size": "M",
          "size_options": ["S", "M", "L"]
        },
        "body": {
          "family": "Open Sans",
          "color": "black",
          "color_options": ["black", "white"],
          "size": "S",
          "size_options": ["S", "M", "L"]
        }
      },
      "content": {
        "basic_info": {
          "groom": {
            "name": "박지훈",
            "max_length": 20,
            "required": true
          },
          "bride": {
            "name": "김하은",
            "max_length": 20,
            "required": true
          },
          "relationship": "평생을 함께할 두 사람"
        },
        "ceremony_details": {
          "date": "2024-06-22",
          "time": "16:30",
          "venue": {
            "name": "모던 컨벤션 센터",
            "address": "서울시 서초구 반포대로 201",
            "contact": "02-2345-6789"
          }
        },
        "additional_info": {
          "parents": {
            "groom_parents": {
              "father": "박상우",
              "mother": "김정희"
            },
            "bride_parents": {
              "father": "김도현",
              "mother": "이미나"
            }
          },
          "message": "새로운 시작을 함께 축하해주세요",
          "contact_info": {
            "phone": "010-2345-6789",
            "email": "jihun.haeun@example.com"
          }
        }
      },
      "metadata": {
        "created_date": new Date("2024-02-10T14:20:00.000Z"),
        "last_modified": new Date("2024-02-10T14:20:00.000Z"),
        "version": "1.0",
        "language": "ko"
      }
    }
  },
  {
    "wedding_invitation": {
      "template": {
        "opening_effect": {
          "enabled": true,
          "lettering_effect": {
            "enabled": true,
            "color": "#8b4513",
            "position": "bottom"
          }
        },
        "design": {
          "template_id": "vintage",
          "frame": {
            "type": "circle",
            "options": ["square", "arch", "circle"]
          },
          "photo": {
            "url": "https://example.com/vintage-photo3.jpg",
            "required": true
          },
          "colors": {
            "background": "#faf0e6",
            "accent": "#8b4513"
          }
        }
      },
      "fonts": {
        "title": {
          "family": "Crimson Text",
          "color": "#654321",
          "size": "L",
          "size_options": ["S", "M", "L"]
        },
        "body": {
          "family": "Libre Baskerville",
          "color": "black",
          "color_options": ["black", "white"],
          "size": "M",
          "size_options": ["S", "M", "L"]
        }
      },
      "content": {
        "basic_info": {
          "groom": {
            "name": "정우성",
            "max_length": 20,
            "required": true
          },
          "bride": {
            "name": "한지민",
            "max_length": 20,
            "required": true
          },
          "relationship": "운명으로 만난 두 사람"
        },
        "ceremony_details": {
          "date": "2024-09-14",
          "time": "11:00",
          "venue": {
            "name": "클래식 가든 홀",
            "address": "서울시 종로구 인사동길 45",
            "contact": "02-3456-7890"
          }
        },
        "additional_info": {
          "parents": {
            "groom_parents": {
              "father": "정재한",
              "mother": "오현주"
            },
            "bride_parents": {
              "father": "한석규",
              "mother": "김영희"
            }
          },
          "message": "오랜 기다림 끝에 만난 소중한 인연, 함께 축복해주세요",
          "contact_info": {
            "phone": "010-3456-7890",
            "email": "woosung.jimin@example.com"
          }
        }
      },
      "metadata": {
        "created_date": new Date("2024-03-20T09:15:00.000Z"),
        "last_modified": new Date("2024-03-20T09:15:00.000Z"),
        "version": "1.0",
        "language": "ko"
      }
    }
  },
  {
    "wedding_invitation": {
      "template": {
        "opening_effect": {
          "enabled": false,
          "lettering_effect": {
            "enabled": true,
            "color": "#4a4a4a",
            "position": "center"
          }
        },
        "design": {
          "template_id": "classic",
          "frame": {
            "type": "square",
            "options": ["square", "arch", "circle"]
          },
          "photo": {
            "url": "https://example.com/classic-photo4.jpg",
            "required": true
          },
          "colors": {
            "background": "#ffffff",
            "accent": "#4a4a4a"
          }
        }
      },
      "fonts": {
        "title": {
          "family": "Times New Roman",
          "color": "#2c3e50",
          "size": "L",
          "size_options": ["S", "M", "L"]
        },
        "body": {
          "family": "Georgia",
          "color": "black",
          "color_options": ["black", "white"],
          "size": "M",
          "size_options": ["S", "M", "L"]
        }
      },
      "content": {
        "basic_info": {
          "groom": {
            "name": "최민호",
            "max_length": 20,
            "required": true
          },
          "bride": {
            "name": "윤서진",
            "max_length": 20,
            "required": true
          },
          "relationship": "서로를 아끼는 두 사람"
        },
        "ceremony_details": {
          "date": "2024-10-05",
          "time": "13:30",
          "venue": {
            "name": "프리미엄 웨딩 플라자",
            "address": "서울시 송파구 올림픽로 300",
            "contact": "02-4567-8901"
          }
        },
        "additional_info": {
          "parents": {
            "groom_parents": {
              "father": "최동일",
              "mother": "신애라"
            },
            "bride_parents": {
              "father": "윤민수",
              "mother": "조은숙"
            }
          },
          "message": "따뜻한 사랑으로 하나가 되는 날, 함께 기쁨을 나누어 주세요",
          "contact_info": {
            "phone": "010-4567-8901",
            "email": "minho.seojin@example.com"
          }
        }
      },
      "metadata": {
        "created_date": new Date("2024-04-12T16:45:00.000Z"),
        "last_modified": new Date("2024-04-12T16:45:00.000Z"),
        "version": "1.0",
        "language": "ko"
      }
    }
  },
  {
    "wedding_invitation": {
      "template": {
        "opening_effect": {
          "enabled": true,
          "lettering_effect": {
            "enabled": true,
            "color": "#e91e63",
            "position": "top"
          }
        },
        "design": {
          "template_id": "romantic",
          "frame": {
            "type": "arch",
            "options": ["square", "arch", "circle"]
          },
          "photo": {
            "url": "https://example.com/romantic-photo5.jpg",
            "required": true
          },
          "colors": {
            "background": "#fdf2f8",
            "accent": "#e91e63"
          }
        }
      },
      "fonts": {
        "title": {
          "family": "Dancing Script",
          "color": "#ad1457",
          "size": "L",
          "size_options": ["S", "M", "L"]
        },
        "body": {
          "family": "Lato",
          "color": "black",
          "color_options": ["black", "white"],
          "size": "M",
          "size_options": ["S", "M", "L"]
        }
      },
      "content": {
        "basic_info": {
          "groom": {
            "name": "임재현",
            "max_length": 20,
            "required": true
          },
          "bride": {
            "name": "송혜교",
            "max_length": 20,
            "required": true
          },
          "relationship": "사랑으로 하나되는 두 사람"
        },
        "ceremony_details": {
          "date": "2024-11-23",
          "time": "15:00",
          "venue": {
            "name": "로맨틱 가든 채플",
            "address": "서울시 마포구 홍익로 88",
            "contact": "02-5678-9012"
          }
        },
        "additional_info": {
          "parents": {
            "groom_parents": {
              "father": "임철수",
              "mother": "배수지"
            },
            "bride_parents": {
              "father": "송일국",
              "mother": "김태희"
            }
          },
          "message": "꽃보다 아름다운 사랑의 약속, 함께 축복해주세요",
          "contact_info": {
            "phone": "010-5678-9012",
            "email": "jaehyun.hyekyo@example.com"
          }
        }
      },
      "metadata": {
        "created_date": new Date("2024-05-08T11:30:00.000Z"),
        "last_modified": new Date("2024-05-08T11:30:00.000Z"),
        "version": "1.0",
        "language": "ko"
      }
    }
  }
];

// Insert the wedding invitation data
print("Inserting wedding invitation data...");
const result = db.wedding.insertMany(weddingInvitations);

// Print the inserted document IDs
print("\nInserted document IDs:");
result.insertedIds.forEach((id, index) => {
  print(`Document ${index + 1}: ${id}`);
});

print(`\nTotal documents inserted: ${result.insertedIds.length}`);

// Verify the data was inserted correctly
print("\nVerifying inserted data...");
const count = db.wedding.countDocuments();
print(`Total documents in wedding collection: ${count}`);

// Display a summary of the inserted data
print("\nSummary of inserted wedding invitations:");
db.wedding.find({}, {
  "_id": 1,
  "wedding_invitation.content.basic_info.groom.name": 1,
  "wedding_invitation.content.basic_info.bride.name": 1,
  "wedding_invitation.template.design.template_id": 1,
  "wedding_invitation.content.ceremony_details.date": 1,
  "wedding_invitation.content.ceremony_details.venue.name": 1
}).forEach(doc => {
  const invitation = doc.wedding_invitation;
  print(`ID: ${doc._id}`);
  print(`  Couple: ${invitation.content.basic_info.groom.name} & ${invitation.content.basic_info.bride.name}`);
  print(`  Template: ${invitation.template.design.template_id}`);
  print(`  Date: ${invitation.content.ceremony_details.date}`);
  print(`  Venue: ${invitation.content.ceremony_details.venue.name}`);
  print("");
});